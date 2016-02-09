using UnityEngine;
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
public enum GameState { Loading, Playing, Pause, GameLost, GameWon }
public interface IGameHandler 
{ 
	event EventHandler <EventArgs> RaiseReborn; 
	event EventHandler <StateEventArg> RaiseChangeState;
	event EventHandler<SoulCollectionEventArg> RaiseSoulCollected;
	void RequestStateHandler(GameState gameState);

	void SetSoulsAmount (int soulAmount);
}
public class GameHandler : StateMachine , IGameHandler
{
	[SerializeField] private UIControllerLevel uiCtrl = null;
	[SerializeField] private CatController catCtrl = null;
	[SerializeField] private GameObject pauseMenu = null;
	[SerializeField] private GameObject endMenu = null;
	[SerializeField] private PauseHandler pauseHandler;
	[SerializeField] private GameObject inGameGUI;
	
	private bool isPause = false;

	public event EventHandler<EventArgs>RaiseReborn;
	protected void OnReborn(EventArgs args){
		if (RaiseReborn != null) {
			RaiseReborn(this, null);		
		}
	}

	public event EventHandler<StateEventArg> RaiseChangeState;
	protected void OnChangeState(StateEventArg arg){
		if (RaiseChangeState != null) {
			RaiseChangeState(this, arg);		
		}
	}
	public event EventHandler<SoulCollectionEventArg> RaiseSoulCollected;
	protected void OnSoulCollected(SoulCollectionEventArg arg)
	{
		if (RaiseSoulCollected != null) 
		{
			RaiseSoulCollected(this, arg);		
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
		this.catCtrl.RaiseReborn += HandleRaiseReborn;
        // Register the pause and end level events
		FindObjectOfType<EndLevel>().RaiseEndLevel += this.OnEnd;
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
		this.catCtrl.Init (this as IGameHandler);
    }
	private void HandleRaiseReborn(object sender, EventArgs arg){
		OnReborn (arg);
	}
    protected void EnterGameWon(Enum oldState)
    {
        //inGameGUI.SetActive(false);
		pauseHandler.enabled = true;
		// Display UI
		endMenu.SetActive(true);
		// Fetch gem collection
		// Add to inventory
		// Save to playerprefs.
    }

    protected void EnterGameLost(Enum oldState)
    {
        endMenu.SetActive(true);
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

	private void OnEnd (object sender, EventArgs arg) 
	{	
		RequestStateHandler(GameState.GameWon);    
	}

	private void HandleFadeInDone (object sender, EventArgs e)
	{
		RequestStateHandler (GameState.Playing);
	}

	public void SetSoulsAmount (int soulAmount)
	{
		OnSoulCollected(new SoulCollectionEventArg(soulAmount));
		this.uiCtrl.SetUISoul (soulAmount);
	}
}

