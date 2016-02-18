using UnityEngine;
using System.Collections;

public class UIControllerFrontPage : UIController 
{
	[SerializeField] private Animator animator = null;
	private void Awake()
	{
		this.fadeController.StartFade ("FadeIn", null);
	}

	public void SetBoard()
	{
		this.animator.Play ("MoveIn");
	}
}
