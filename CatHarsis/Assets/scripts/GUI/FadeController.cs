using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class FadeController : MonoBehaviour
{	
	private System.Action action = null;
	private Animator animator = null;

	private void Awake()
	{
		if (this.animator == null) {
			this.animator = this.gameObject.GetComponent<Animator> ();
		}
	}

	public void StartFade (string parameter, System.Action onFadeDone)
	{
		action = onFadeDone;
		if (this.animator == null) {
			this.animator = this.gameObject.GetComponent<Animator> ();
		}
		this.animator.SetBool (parameter, true);
	}
	public void OnFadeoutDone()
	{
		this.animator.SetBool ("FadeOut", false);
		if (action != null) 
		{
			action();
		}
		action = null;
	}

	public void OnFadeInDone()
	{
		this.animator.SetBool ("FadeIn", false);
		if (action != null) 
		{
			action();
		}
		action = null;
	}
}



