using UnityEngine;
using System.Collections;

public class TutorialCatController : MonoBehaviour , IInputListener
{
	[SerializeField] private InputController inputCtrl = null;
	[SerializeField] private CatMove catMove = null;

	public void RegisterMovement()
	{
		this.inputCtrl.RegisterSingleTap (this);
	}
	public void UnregisterMovement()
	{
		this.inputCtrl.UnregisterSingleTap (this);
	}
	public void RegisterJump()
	{
		this.inputCtrl.RegisterDoubleTap (this);
	}
	public void UnregisterJump()
	{
		this.inputCtrl.UnregisterDoubleTap (this);
	}
	private void HandleOnMovementCall (Vector3 value)
	{
		this.catMove.Move (value);
	}

	public void HandleSingleTap(Vector3 vec)
	{
		Ray ray = Camera.main.ScreenPointToRay (vec);
		Vector3 target = ray.origin;
		target.z = 0f;
		this.catMove.Move (target);
	}
	public void HandlerDoubleTap()
	{
		this.catMove.Jump ();
	}
}
