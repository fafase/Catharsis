using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulMonobehaviour : MonoBehaviour {
    public delegate void TransitionToStateDelegate(object behaviour, string oldstate, string newstate);
    public event TransitionToStateDelegate StateTransitionListeners;

    internal class State {
        public string name;
        public List<string> allowedTransitions;
        public bool allowAnyTransition;
        public MethodInfo updateMethod;
        public MethodInfo enterMethod;
        public MethodInfo exitMethod;

        public State(string name) {
            this.name = name;
            this.allowAnyTransition = false;
            this.allowedTransitions = new List<string>();
            this.updateMethod = null;
            this.enterMethod = null;
            this.exitMethod = null;
        }
    }

    private Dictionary<string, State> legalStates; 
 
    private State CurrentState;
    private bool inTransition;
    private State transitionSource;
    private State transitionTarget;
    private bool statefulnessInitialized = false;
    private bool debugTransitions;

    public string CurrentStateName {
        get { return CurrentState.name; }
    }

    protected void InitializeStatefulness( bool debug ) {
        if (statefulnessInitialized) {
            Debug.LogWarning( GetType().ToString() + " is trying to initialize statefulness multiple times." );
            return;
        }

        this.legalStates = new Dictionary<string, State>();

        // create an initial state that can transition into any state but cannot be transitioned to
        this.AddState( "InitialState" );
        State initial = legalStates["InitialState"];
        initial.allowAnyTransition = true;

        CurrentState = initial;
        inTransition = false;
        transitionSource = null;
        transitionTarget = null;
        statefulnessInitialized = true;
        debugTransitions = debug;
    }

    protected bool IsLegalTransition(string fromstate, string tostate) {
        if (legalStates.ContainsKey(fromstate) && legalStates.ContainsKey(tostate)) {
            if (legalStates[fromstate].allowAnyTransition || legalStates[fromstate].allowedTransitions.Contains(tostate)) {
                return true;
            }
        }

        return false;
    }
    
    private void TransitionTo(string newstate) {
        transitionSource = CurrentState;
        transitionTarget = legalStates[newstate];
        inTransition = true;

        MethodInfo old_exit = CurrentState.exitMethod;
        MethodInfo new_enter = transitionTarget.enterMethod;

        if (old_exit != null) {
            old_exit.Invoke( this, new object[] { transitionTarget.name } );            
        }
         
        if (new_enter != null) {
            new_enter.Invoke( this, new object[] { CurrentState.name } );            
        }

        CurrentState = transitionTarget;
    }

    private void FinalizeCurrentTransition() {
        if( transitionTarget == null || transitionSource == null) {
            Debug.LogError( this.GetType().ToString() + " cannot finalize transition; source or target state is null!");
            return;
        }

        DidTransitionToNewState( transitionSource.name, transitionTarget.name );
        if( StateTransitionListeners != null )
            StateTransitionListeners( this, transitionSource.name, transitionTarget.name );

        inTransition = false;
        transitionSource = null;
        transitionTarget = null;
    }
    
    protected bool RequestState(string newstate) {
        if (!statefulnessInitialized) {
            Debug.LogError( this.GetType().ToString()+ " requests transition to state " + newstate + " but statefulness is not initialized!");
            return false;            
        }

        if (inTransition) {
            // cannot transition when transitioning
            // TODO: maybe we should stick the state request into a queue here?
            if (debugTransitions)
                Debug.Log(this.GetType().ToString() + " requests transition to state " + newstate +
                          " when still transitioning to state " + transitionTarget.name);
            return false;
        }

        if (CurrentState.name == newstate) {
            // null transition, just ignore it
            // return value here is debatable: on one hand nothing went wrong, 
            // but on the other hand we did not do any transitioning!
            return true;  
        }

        if( IsLegalTransition( CurrentState.name, newstate ) && this.WillTransitionToNewState( CurrentState.name, newstate ) ) {
            // we are ready to transition so lets go!
            if (debugTransitions) {
                Debug.Log( this.GetType().ToString() + " transition: " + CurrentState.name + " => " + newstate );
            }

            TransitionTo(newstate);
            FinalizeCurrentTransition();
        } else {
            Debug.LogError( this.GetType().ToString() + " requests transition: " + CurrentState.name + " => " + newstate + " but it is not a legal transition!" );
            return false;
        }

        return true;
    }

    protected void AddState(string newstate) {
        State s = new State( newstate );
        System.Type ourType = this.GetType(); 

        s.updateMethod = ourType.GetMethod("Update"+newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );
        s.enterMethod = ourType.GetMethod( "EnterState" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );
        s.exitMethod = ourType.GetMethod( "ExitState" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );

        this.legalStates.Add( newstate, s );
    }

    protected void AddStateWithTransitions(string newstate, string[] transitions) {
        AddState(newstate);
        State s = legalStates[newstate];

        foreach (string t in transitions) {
            s.allowedTransitions.Add( t );
        }
    }

    protected virtual bool WillTransitionToNewState(string fromstate, string tostate) {
        return true;
    }

    protected virtual void DidTransitionToNewState(string fromstate, string tostate) {
    }

    protected virtual void StateUpdate() {
        // call the update method of the current state

        if (statefulnessInitialized && CurrentState != null) { 
            MethodInfo meth = CurrentState.updateMethod; 
            if( meth != null )
                meth.Invoke( this, null );
        }
    }
}
