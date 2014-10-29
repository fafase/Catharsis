using UnityEngine;
using System.Collections;
using System;


/*	GAMEHANDLER
 *	This class is intended to handle all common values and actions that persist through levels.
 *	It should be attached to the mainCamera!
 *
 *	Features included are, for example:
 *	- Death counter
 *	- Death handler

 */

public class GameHandler : StatefulMonobehaviour 
{
    public static Action<string> OnChangeState = (string s)=>{};
    private static GameHandler instance;
	public static GameHandler Instance{
		get
		{
			return instance;
		}
	}
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }

        InitializeStatefulness(true);
        AddStateWithTransitions(Utility.GAME_STATE_LOADING, new string []{ Utility.GAME_STATE_PLAYING });
        AddStateWithTransitions(Utility.GAME_STATE_PLAYING, new string[]{Utility.GAME_STATE_PAUSE, Utility.GAME_STATE_GAMEWON, Utility.GAME_STATE_GAMELOST});
        AddStateWithTransitions(Utility.GAME_STATE_PAUSE, new string[]{Utility.GAME_STATE_PLAYING});
        AddStateWithTransitions(Utility.GAME_STATE_GAMELOST, new string[]{Utility.GAME_STATE_LOADING});
        AddStateWithTransitions(Utility.GAME_STATE_GAMEWON, new string[]{Utility.GAME_STATE_LOADING});
        RequestStateHandler(Utility.GAME_STATE_LOADING);
    }
    void Update() 
    {
        StateUpdate();
    }
    protected virtual void EnterStateLoading(string oldState)
    {
        timer = 2.0f;
    }
    private float timer;
    protected virtual void UpdateLoading() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            RequestStateHandler(Utility.GAME_STATE_PLAYING);
        }
    }

    
    private void RequestStateHandler(string state)
    {
        RequestState(state);
        OnChangeState(CurrentStateName);
    }
}

