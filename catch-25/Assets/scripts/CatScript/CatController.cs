using UnityEngine;
using System.Collections;

public class CatController : StatefulMonobehaviour 
{
	[SerializeField] private CatMovement catMoveRef;
	[SerializeField] private CatHealth catHealth;
	[SerializeField] private GameObject catPrefab;
    [SerializeField] private Transform jellyPosition;

	// Use this for initialization
	void Start () 
	{
		InputManager.OnMovement += catMoveRef.Move;
		InputManager.OnJump += catMoveRef.Jump;
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
        Instantiate(catPrefab, transform.position, Quaternion.identity);
        renderer.enabled = false;
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
        Vector3 position = jellyPosition.position;
        position.y -= 1f;
        transform.position = position;
        renderer.enabled = true;
        RequestState(Utility.STATE_PLAYING);
    }
	private void DeadCatClone()
	{
        RequestState(Utility.STATE_RESET);
		
	} 
}
