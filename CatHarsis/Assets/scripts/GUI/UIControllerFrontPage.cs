using UnityEngine;
using System.Collections;

public class UIControllerFrontPage : UIController 
{
	[SerializeField] private Animator animator = null;
	[SerializeField] private GameObject tutorialFilter = null;

	private void Awake()
	{
		this.fadeController.StartFade ("FadeIn", null);
		int value = PlayerPrefs.GetInt ("RemoveTutorial",-1);
		if (value > 0) 
		{
			Destroy (tutorialFilter.gameObject);
		}
	}

	public void SetBoard()
	{
		this.animator.Play ("MoveIn");
	}
}
