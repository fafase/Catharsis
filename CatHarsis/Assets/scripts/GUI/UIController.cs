using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
	[SerializeField] private FadeController fadeController = null;
	private string level = null;

	private void Start()
	{
		this.fadeController.StartFade ("FadeIn", OnFadeInDone);
	}

	public void SetLevel (string parameter, string newLevel)
	{
		this.level = newLevel;
		this.fadeController.StartFade (parameter, OnFadeDone);
	}

	private void OnFadeDone()
	{
		if (this.level == null) 
		{
			return;		
		}
		Application.LoadLevel (this.level);
		this.level = null;
	}

	private void OnFadeInDone()
	{

	}
}
