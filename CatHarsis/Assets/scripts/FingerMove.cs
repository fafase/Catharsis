using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class FingerMove : MonoBehaviour
{
	[SerializeField] private Animator animator = null;

	private void Awake()
	{
		Idle ();
	}
	public void Idle()
	{
		this.animator.Play ("Idle");
	}
	public void SingleTap(){
		this.animator.Play ("SingleTap");
	}
	public void DoubleTap(){this.animator.Play ("DoubleTap");}
}
