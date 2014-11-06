using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// GameHandler class is attached to the GameManager game object
/// It is a singleton class and a StatefulMonobehaviour type
/// </summary>
public class GameHandler : StatefulMonobehaviour 
{
    [SerializeField]
    private GameObject pauseMenu;
	[SerializeField]
	private GameObject endMenu;
    [SerializeField]
    private PauseHandler pauseHandler;
    [SerializeField]
    private GameObject inGameGUI;
    public Action<string> OnChangeState = (string s)=>{};
    private bool isPause = false;
    private float timer;
   
    // Singleton part
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

        SetGlobalSettings();
      
        // Register the pause and end level events
        FindObjectOfType<InputManager>().OnPause += this.OnPause;
		FindObjectOfType<EndLevel>().OnEnd += this.OnEnd;
        
        // Register all states of the game
        InitializeStateMachine(true);
        AddStateWithTransitions(Utility.GAME_STATE_LOADING, new string []{ Utility.GAME_STATE_PLAYING });
        AddStateWithTransitions(Utility.GAME_STATE_PLAYING, new string[]{Utility.GAME_STATE_PAUSE, Utility.GAME_STATE_GAMEWON, Utility.GAME_STATE_GAMELOST});
        AddStateWithTransitions(Utility.GAME_STATE_PAUSE, new string[]{Utility.GAME_STATE_PLAYING});
        AddStateWithTransitions(Utility.GAME_STATE_GAMELOST, new string[]{Utility.GAME_STATE_LOADING});
        AddStateWithTransitions(Utility.GAME_STATE_GAMEWON, new string[]{Utility.GAME_STATE_LOADING});
        RequestStateHandler(Utility.GAME_STATE_LOADING);

        // Disable pause and endMenu GUI
        pauseHandler.enabled = false;
        pauseMenu.SetActive(false);
		endMenu.SetActive (false);
    }

    // Update method only calls the StateUpdate from StatefulMB
    void Update() 
    {
        StateUpdate();
    }

    // Section for the various Enter/Exit/Update states
    // Each method is called in the corresponding state at the corresponding moment of the state
    protected virtual void EnterLoading(string oldState)
    {
        timer = 2.0f;
    }

    protected virtual void UpdateLoading() 
    {
        timer -= Time.deltaTime; 
        if (timer <= 0.0f)
        {
            RequestStateHandler(Utility.GAME_STATE_PLAYING);
        }
    }

    protected void EnterGameWon(string oldState)
    {
        inGameGUI.SetActive(false);
        endMenu.SetActive(true);
        pauseHandler.enabled = true;
        Transform tr = endMenu.transform.Find("TextInfo");
        tr.GetComponent<GUIText>().text = "Press R to restart";
        StartCoroutine(FadeInEndScreen(endMenu));
    }

    protected void EnterGameLost(string oldState)
    {
        endMenu.SetActive(true);
        StartCoroutine(FadeInEndScreen(endMenu));
    }

    public void RequestStateHandler(string state)
    {
        RequestState(state);
        OnChangeState(CurrentState);
    }

    private void SetGlobalSettings()
    {
        // Set global settings for game
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Application.runInBackground = true;
    }

    private void OnPause() 
    {
        isPause = !isPause;
        inGameGUI.SetActive(!isPause);
        string state =  (isPause == true) ? Utility.GAME_STATE_PAUSE : Utility.GAME_STATE_PLAYING;
        Time.timeScale = (isPause == true) ? 0.0f : 1.0f;
        pauseMenu.SetActive(isPause);
        pauseHandler.enabled = isPause;
        RequestStateHandler(state);
    }
    
    // OnEnd is called when EndLevel is called
    // RequestStateHandler will propagate the message to listeners
	private void OnEnd () 
	{	
		RequestStateHandler(Utility.GAME_STATE_GAMEWON);       
	}
    
    private IEnumerator FadeInEndScreen(GameObject panel)
    {
        GUITexture [] guiTextures = panel.GetComponentsInChildren<GUITexture>();
        GUIText [] guiTexts = panel.GetComponentsInChildren<GUIText>();
        
        for (int i = 0; i < guiTextures.Length; i++)
        {
            Color col = guiTextures[i].color;
            col.a = 0f;
            guiTextures[i].color = col;
        }
        for (int i = 0; i < guiTexts.Length; i++)
        {
            Color col = guiTexts[i].color;
            col.a = 0f;
            guiTexts[i].color = col;
        }
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * 0.5f; 
            for (int i = 0; i < guiTextures.Length; i++)
            {
                Color col = guiTextures[i].color;
                col.a = timer;
                guiTextures[i].color = col;
            }
            for (int i = 0; i < guiTexts.Length; i++)
            {
                Color col = guiTexts[i].color;
                col.a = timer;
                guiTexts[i].color = col;
            }
            yield return null;
        }
    }
}

