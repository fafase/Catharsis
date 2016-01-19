using UnityEngine;
using System.Collections;

public class TutorialCatController : MonoBehaviour 
{
	[SerializeField] private InputManager inputManager = null;
	[SerializeField] private CatMove catMove = null;
	private void Awake()
	{
		//RegisterMovement ();
	}

	public void RegisterMovement()
	{
		this.inputManager.OnSingleTap += HandleOnMovementCall;
		this.inputManager.OnDoubleTap += HandleOnTouch;
	}

	void HandleOnTouch ()
	{
		this.catMove.Jump ();
	}
	public void UnregisterMovement()
	{
		this.inputManager.OnSingleTap -= HandleOnMovementCall;
	}
	private void HandleOnMovementCall (Vector3 value)
	{
		this.catMove.Move (value);
	}
}
