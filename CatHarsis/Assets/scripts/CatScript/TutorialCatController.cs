using UnityEngine;
using System.Collections;

public class TutorialCatController : MonoBehaviour , IInputListener
{
	[SerializeField] private InputController inputCtrl = null;
	[SerializeField] private CatMove catMove = null;
	private void Awake()
	{
		this.target = this.catMove.transform.position;
	}
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
		if (this.coroutine != null) {
			return;
		}
		Debug.Log ("Handle");
		Ray ray = Camera.main.ScreenPointToRay (vec);
		Vector3 target = ray.origin;
		target.z = 0f;
		this.catMove.Move (target);
	}

	public void HandlerDoubleTap()
	{
		this.catMove.Jump ();
	}
	private IEnumerator coroutine = null;
	public void MoveToPosition(System.Action onCompletion)
	{
		Debug.Log ("Call");
		this.coroutine = MoveToPositionCoroutine (onCompletion);
		StartCoroutine (this.coroutine);
	}
	Vector3 target;
	private IEnumerator MoveToPositionCoroutine(System.Action onCompletion)
	{
		Debug.Log ("Move back");
		this.catMove.Move (this.target, true);
	
		while (Mathf.Abs (this.target.x - this.transform.position.x) > 0.5f) 
		{
			//this.catMove.Move (this.target, true);
			Debug.Log ("Moving");
			yield return null;
		}
		this.catMove.StopMovement ();
		if (this.catMove.transform.localScale.x < 0) 
		{
			this.catMove.Flip();
		}

		if (onCompletion != null) 
		{
			onCompletion ();
		}
		this.coroutine = null;
	}
}
