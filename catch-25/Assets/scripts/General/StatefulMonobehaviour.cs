using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulMonobehaviour : MonoBehaviour {
    public event Action<object, string, string> OnStateTransition = 
        (object behaviour, string oldState, string newState) => { };

    internal class State {
        public string name;
        public List<string> allowedTransitions;
        public bool allowAnyTransition;
        public Action updateMethod = ()=>{};
        public Action<string> enterMethod = (string oldState) => { };
        public Action<string> exitMethod = (string oldState) => { };

        public State(string name) {
            this.name = name;
            this.allowAnyTransition = false;
            this.allowedTransitions = new List<string>();
        }
    }

    private Dictionary<string, State> legalStates; 
 
    private State CurrentState;
    private bool inTransition;
    private State transitionSource;
    private State transitionTarget;
    private bool statefulnessInitialized = false;
    private bool debugTransitions;

    private Action OnUpdate = () => { };

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

        CurrentState.exitMethod(transitionTarget.name);
        transitionTarget.enterMethod(CurrentState.name);
        CurrentState = transitionTarget;
    }

    private void FinalizeCurrentTransition() {
        if (transitionTarget == null || transitionSource == null)
        {
            Debug.LogError(this.GetType().ToString() + " cannot finalize transition; source or target state is null!");
            return;
        }
        OnStateTransition( this, transitionSource.name, transitionTarget.name );

        inTransition = false;
        transitionSource = null;
        transitionTarget = null;
    }
    
    protected bool RequestState(string newstate) {
        if (!statefulnessInitialized) {
            Debug.LogError( this.GetType().ToString()+ " requests transition to state " + newstate + " but statefulness is not initialized!");
            return false;            
        }

        if (inTransition) 
        {
            if (debugTransitions)
                Debug.Log(this.GetType().ToString() + " requests transition to state " + newstate +
                          " when still transitioning to state " + transitionTarget.name);
            return false;
        }

        if( IsLegalTransition( CurrentState.name, newstate )) 
        {
            // we are ready to transition so lets go!
            if (debugTransitions) 
            {
                Debug.Log( this.GetType().ToString() + " transition: " + CurrentState.name + " => " + newstate );
            }

            TransitionTo(newstate);
            FinalizeCurrentTransition();
        } else 
        {
            Debug.LogError( this.GetType().ToString() + " requests transition: " + CurrentState.name + " => " + newstate + " but it is not a legal transition!" );
            return false;
        }
        OnUpdate = null;
        OnUpdate = CurrentState.updateMethod;
        return true;
    }

    protected void AddState(string newstate) 
    {
        State s = new State( newstate );
        System.Type ourType = this.GetType(); 

        MethodInfo update = ourType.GetMethod("Update" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        MethodInfo enter = ourType.GetMethod("Enter" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        MethodInfo exit = ourType.GetMethod("Exit" + newstate, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        
        if (update != null)
        {
            s.updateMethod = (Action)Delegate.CreateDelegate(typeof(Action), this, update);
        }
        if (enter != null)
        {
            s.enterMethod = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), this,enter);
        }
        if (exit != null)
        {
            s.exitMethod = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), this, exit);
        }
        this.legalStates.Add( newstate, s );
    }

    protected void AddStateWithTransitions(string newstate, string[] transitions) 
    {
        AddState(newstate);
        State s = legalStates[newstate];

        foreach (string t in transitions) 
        {
            s.allowedTransitions.Add( t );
        }
    }

    protected virtual void StateUpdate() 
    {
        OnUpdate();
    }
}
