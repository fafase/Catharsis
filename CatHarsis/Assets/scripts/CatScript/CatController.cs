using UnityEngine;
using System.Collections;

/// <summary>
/// CatController is attached to the cat. It is the higher layer of the cat object and the only component to communicate 
/// with the other cat component and the component outside the cat object.
/// It is of type StatefulMonobehaviour and controls the states of the cat. 
/// It also communicates witht he GameHandler to update the game state, mainly if the cat is dead. 
/// </summary>
using System;


public class CatController : StateMachine, IInputListener
{
	public EventHandler<EventArgs> RaiseReborn;
	protected void OnReborn (EventArgs args)
	{
		if (RaiseReborn != null) {
			RaiseReborn(this, args);		
		}
	}

	[SerializeField] private CatMove catMoveRef;
	[SerializeField] private CatHealth catHealth;
   
	[SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform jellyPosition;
	[SerializeField] private float resetTimer = 1.0f;
	[SerializeField] private InputController inputController = null;

    private IGameHandler gameHandler = null;
    private SpriteRenderer spriteRenderer;
    private int coins;
    float timer = 1f;
	private CatInventory catInventory;
	CatDeath catDeath = CatDeath.None;
	enum CatState { OnHold, Starting, Playing, Pause, Reset, Poisoned, Dead }

	public void Init (IGameHandler gameHandler) 
	{
		InitStateMachine ();
		this.gameHandler = gameHandler;
        if (this.gameHandler != null)
        {
            this.gameHandler.RaiseChangeState += OnGameHandlerChangeState;
        }
        DeathTrigger.RaiseDeath += ResetOnDeath;

		this.spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		this.spriteRenderer.enabled = false;
		this.catInventory = new CatInventory ();
	}

	private void OnDestroy()
	{
		this.catInventory = null;
	}

	public void SetSoulsAmount(int value)
	{
		int soulAmount =this.catInventory.SetSoulsAmount (value);
		this.gameHandler.SetSoulsAmount (soulAmount);
	}

	private void InitStateMachine()
	{
		InitializeStateMachine<CatState> (CatState.OnHold, true);
		AddTransitionsToState (CatState.OnHold, new Enum[] { CatState.Starting });
		AddTransitionsToState(CatState.Starting, new Enum[]{CatState.Playing});
		AddTransitionsToState(CatState.Playing, new Enum[] { CatState.Pause, CatState.Reset,CatState.Poisoned, CatState.Dead });
		AddTransitionsToState(CatState.Reset, new Enum[]{ CatState.Starting, CatState.Playing, CatState.Pause});
		AddTransitionsToState(CatState.Dead, new Enum[] { CatState.Starting });
		AddTransitionsToState(CatState.Pause, new Enum[]{CatState.Playing, CatState.Reset,CatState.Starting});
		AddTransitionsToState(CatState.Poisoned, new Enum[] { CatState.Reset});
	}
    public void PoisonCat()
    {
        if ((CatState)CurrentState == CatState.Poisoned) 
        {
            return;
        }
        ChangeCurrentState(CatState.Poisoned);
    }
	protected void EnterStarting(Enum previous)
	{
		if (this.jellyPosition != null) 
		{
			OnReborn(null);
			Vector3 position = jellyPosition.position;
			transform.position = position;
			this.catMoveRef.enabled = true;
		}
	}
	protected void UpdateStarting()
	{
		if (this.catMoveRef.IsGrounded == true) 
		{
			ChangeCurrentState(CatState.Playing);		
		}
	}
	protected void EnterPause(Enum previous)
	{
		this.catMoveRef.Pause ();
	}
	protected virtual void EnterPlaying(Enum oldState)
    {
        if (AudioManager.Instance != null)
        {
			AudioManager.Instance.PlayAudio("respawn", 1.0f, 1.0f);
        }
        catMoveRef.enabled = true;
        spriteRenderer.color = Color.white;
		this.inputController.Register (this);
		this.catMoveRef.ResetToPlay (this.transform.position);
    }
    // Exiting playing we unsuscribe the control so that Pause or death disable controls

    protected virtual void ExitPlaying(Enum nextState)
    {
        if ((CatState)nextState != CatState.Poisoned)
        {
			this.inputController.Unregister(this);
        }
    }
    // Entering the reset state, collider is disabled to avoid multiple collision
    // timer is set and control are unregistered
    protected virtual void EnterReset(Enum oldState) 
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        catMoveRef.enabled = false;
        timer = resetTimer;
		this.inputController.Unregister (this);
    }

    protected virtual void UpdateReset() 
    {
        // Decrease timer until 0
        timer -= Time.deltaTime;
        if (timer <= 0.0f) 
        {
            ChangeCurrentState(CatState.Starting);
        }
    }
    protected virtual void ExitReset(Enum nextState)
    {
        // if we require a dead corpse
        if (catDeath != CatDeath.Fall)
        {
            GameObject obj = (GameObject)Instantiate(catPrefab, transform.position, Quaternion.identity);
			obj.GetComponent<DeadCatController>().InitDeadCat(catDeath, transform.localScale.x);           
        }
        // Place on the jelly

        
        // Set the collider and rigidbody
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
    protected virtual void EnterPoisoned(Enum oldState)
    {
        timer = 3f;
    }
    protected virtual void UpdatePoisoned()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(0.1f,0.6f,0.1f,1.0f), Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            ResetOnDeath(null, new CatDeathEventArg(CatDeath.Gas));
        }
    }
    private void OnGameHandlerChangeState(object sender, StateEventArg arg)
    {
		GameState newState = (GameState)arg.currentState;
		if (newState == GameState.Playing)
        {
			this.spriteRenderer.enabled = true;
            ChangeCurrentState(CatState.Starting);
        }
		else if (newState == GameState.Pause || newState == GameState.GameWon)
        {
			ChangeCurrentState(CatState.Pause);
        }
		else if (newState == GameState.GameLost)
        {
			ChangeCurrentState(CatState.Dead);
        }
    }

	public void HandleSingleTap(Vector3 vec)
	{
		Ray ray = Camera.main.ScreenPointToRay (vec);
		Vector3 target = ray.origin;
		target.z = 0f;
		this.catMoveRef.Move (target);
	}
	public void HandlerDoubleTap()
	{
		this.catMoveRef.Jump ();
	}

    private void ResetOnDeath(object sender, CatDeathEventArg arg)
    {
        catMoveRef.ResetOnDeath();
        int life = catHealth.DecreaseHealth();
		this.catDeath = arg.catDeath;
        if (life >= 0)
        {
            ChangeCurrentState(CatState.Reset);
        }
        else
        {
			this.gameHandler.RequestStateHandler(GameState.GameLost);
        }
    } 
}
