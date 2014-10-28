using UnityEngine;
using System.Collections;

public class CatController : StatefulMonobehaviour 
{
	[SerializeField] private CatMove catMoveRef;
	[SerializeField] private CatHealth catHealth;
	[SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform jellyPosition;

	void Start () 
	{
	    InputManager.OnMovementCall += catMoveRef.Move;     
		InputManager.OnJumpCall += catMoveRef.Jump;
		
        DeathTrigger.OnDeath += DeadCatClone;
        
        InitializeStatefulness(true);
        AddStateWithTransitions(Utility.STATE_STARTING, new string[]{Utility.STATE_PLAYING});
        AddStateWithTransitions(Utility.STATE_PLAYING, new string[] { Utility.STATE_PAUSE, Utility.STATE_RESET, Utility.STATE_DEAD });
        AddStateWithTransitions(Utility.STATE_RESET, new string[]{Utility.STATE_PLAYING, Utility.STATE_PAUSE});
        AddStateWithTransitions(Utility.STATE_DEAD, new string[] { Utility.STATE_STARTING});
        AddStateWithTransitions(Utility.STATE_PAUSE, new string[]{Utility.STATE_PLAYING, Utility.STATE_RESET});      
        RequestState(Utility.STATE_PLAYING);
        
	}

    protected virtual void Update() 
    {
        StateUpdate(); 
    }

    protected virtual void EnterStateReset(string oldState) 
    {
        catHealth.DecreaseHealth();
        resetTimer = 1.0f;
    }
    float resetTimer = 1.0f;
    protected virtual void UpdateReset() 
    {
        resetTimer -= Time.deltaTime;
        if (resetTimer > 0.0f) 
        {
            return;
        }
        Instantiate(catPrefab, transform.position, Quaternion.identity);

        Vector3 position = jellyPosition.position;
        position.y -= 0.5f;
        transform.position = position;
        catMoveRef.ResetToPlay();
        RequestState(Utility.STATE_PLAYING);
    }
	private void DeadCatClone()
	{
        RequestState(Utility.STATE_RESET);
	} 
}
