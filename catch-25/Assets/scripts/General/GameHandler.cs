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
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private FadeController fade;
    [SerializeField]
    private PauseHandler pauseHandler;
    public Action<string> OnChangeState = (string s)=>{};
    
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

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        pauseHandler.enabled = false;
        
        FindObjectOfType<InputManager>().OnPause += this.OnPause;
        InitializeStatefulness(true);
        AddStateWithTransitions(Utility.GAME_STATE_LOADING, new string []{ Utility.GAME_STATE_PLAYING });
        AddStateWithTransitions(Utility.GAME_STATE_PLAYING, new string[]{Utility.GAME_STATE_PAUSE, Utility.GAME_STATE_GAMEWON, Utility.GAME_STATE_GAMELOST});
        AddStateWithTransitions(Utility.GAME_STATE_PAUSE, new string[]{Utility.GAME_STATE_PLAYING});
        AddStateWithTransitions(Utility.GAME_STATE_GAMELOST, new string[]{Utility.GAME_STATE_LOADING});
        AddStateWithTransitions(Utility.GAME_STATE_GAMEWON, new string[]{Utility.GAME_STATE_LOADING});
        RequestStateHandler(Utility.GAME_STATE_LOADING);

        fade.ChangeFadeState(FadeController.FadeState.FadeIn);

        pauseMenu.SetActive(false);
    }
    void Update() 
    {
        StateUpdate();
    }
    protected virtual void EnterStateLoading(string oldState)
    {
        timer = 2.5f;
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
  
    public void RequestStateHandler(string state)
    {
        RequestState(state);
        OnChangeState(CurrentStateName);
    }
    private bool isPause = false;
    private void OnPause() 
    {
        isPause = !isPause;
        string state =  (isPause == true) ? Utility.GAME_STATE_PAUSE : Utility.GAME_STATE_PLAYING;
        Time.timeScale = (isPause == true) ? 0.0f : 1.0f;
        pauseMenu.SetActive(isPause);
        pauseHandler.enabled = isPause;
        RequestStateHandler(state);
    }

}

