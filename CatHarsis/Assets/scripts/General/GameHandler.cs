﻿using UnityEngine;
using System.Collections;
using System;

public class StateEventArg : System.EventArgs
{
	public readonly  Enum currentState;
	public StateEventArg(Enum currentState)
	{
		this.currentState = currentState;
	}
}
public enum GameState { Loading , Playing, Pause, GameLost, GameWon }

public class GameHandler : StateMachine
{
	[SerializeField] private UIController uiCtrl = null;


    [SerializeField] private GameObject pauseMenu = null;
	[SerializeField] private GameObject endMenu = null;
    [SerializeField] private PauseHandler pauseHandler;
    [SerializeField] private GameObject inGameGUI;
	private bool isPause = false;
	//[SerializeField] private float loadingTimer = 0.0f;

	public EventHandler<StateEventArg> RaiseChangeState;
	protected void OnChangeState(StateEventArg arg){
		if (RaiseChangeState != null) {
			RaiseChangeState(this, arg);		
		}
	}
   
    // Singleton part
    private static GameHandler instance = null;
	public static GameHandler Instance
	{
		get
		{
			return instance;
		}
	}
    
    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
      
        // Register the pause and end level events
		FindObjectOfType<EndLevel>().OnEnd += this.OnEnd;
		this.uiCtrl.RaiseFadeInDone += HandleFadeInDone;
		this.uiCtrl.StartFade ();
        // Register all states of the game
        InitializeStateMachine<GameState>(GameState.Loading,true);
		AddTransitionsToState(GameState.Loading, new Enum []{ GameState.Playing,GameState.Pause });
		AddTransitionsToState(GameState.Playing, new Enum[]{GameState.Pause, GameState.GameWon, GameState.GameLost});
		AddTransitionsToState(GameState.Pause, new Enum[]{GameState.Playing, GameState.Loading});
		AddTransitionsToState(GameState.GameLost, new Enum[]{GameState.Loading});
		AddTransitionsToState(GameState.GameWon, new Enum[]{GameState.Loading});

        // Disable pause and endMenu GUI
        pauseHandler.enabled = false;
        pauseMenu.SetActive(false);
		endMenu.SetActive (false);

    }

    /*protected virtual void UpdateLoading() 
    {
        loadingTimer -= Time.deltaTime; 
        if (loadingTimer <= 0.0f)
        {
			RequestStateHandler(GameState.Playing);
        }
    }*/

    protected void EnterGameWon(Enum oldState)
    {
        inGameGUI.SetActive(false);
        endMenu.SetActive(true);
        pauseHandler.enabled = true;
        Transform tr = endMenu.transform.Find("TextInfo");
        tr.GetComponent<GUIText>().text = "Press R to restart";
        //StartCoroutine(FadeInEndScreen(endMenu));
    }

    protected void EnterGameLost(Enum oldState)
    {
        endMenu.SetActive(true);
        //StartCoroutine(FadeInEndScreen(endMenu));
    }

    public void RequestStateHandler(GameState state)
    {
        ChangeCurrentState(state);
        OnChangeState(new StateEventArg(CurrentState));
    }

    public void OnPause() 
    {
        isPause = !isPause;
        inGameGUI.SetActive(!isPause);
		GameState state =  (isPause == true) ? GameState.Pause : GameState.Playing;
        Time.timeScale = (isPause == true) ? 0.0f : 1.0f;
        pauseMenu.SetActive(isPause);
        pauseHandler.enabled = isPause;
        RequestStateHandler(state);
    }
    
    // OnEnd is called when EndLevel is called
    // RequestStateHandler will propagate the message to listeners
	private void OnEnd () 
	{	
		RequestStateHandler(GameState.GameWon);       
	}

	private void HandleFadeInDone (object sender, EventArgs e)
	{
		OnChangeState(new StateEventArg(GameState.Playing));
	}
}

