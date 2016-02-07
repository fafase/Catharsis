using UnityEngine;
using System.Collections;

public class JellyController : MonoBehaviour 
{
	[SerializeField] private GameHandler gameHandler = null;
	[SerializeField] private Animator animator = null;

	private void Awake()
	{
		this.gameHandler.RaiseReborn += HandleReborn;
		this.animator.GetComponentInChildren<SpriteRenderer>().enabled = false;
	}

	private void OnDestroy()
	{
		this.gameHandler.RaiseReborn += HandleReborn;
		this.gameHandler = null;
	}

	private void HandleReborn(object sender, System.EventArgs arg)
	{
		this.animator.GetComponentInChildren<SpriteRenderer>().enabled = true;
		this.animator.Play("SmokeMovement",0, 0.0f);
	}
}
