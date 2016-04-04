using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	[SerializeField] protected FadeController fadeController = null;
	protected string level = null;

	public event EventHandler<EventArgs> RaiseFadeOutDone;
	public event EventHandler<EventArgs> RaiseFadeInDone;

	protected virtual void OnFadeInDone(EventArgs arg)
	{
		if (RaiseFadeInDone != null) 
		{
			RaiseFadeInDone(this, arg);
		}
	}

	protected virtual void OnFadeOutDone(EventArgs arg)
	{
		if (RaiseFadeOutDone != null) 
		{
			RaiseFadeOutDone(this, arg);
		}
	}
	public virtual void StartFade()
	{
		this.fadeController.StartFade ("FadeIn", FadeInDone);
	}

	public virtual void SetLevel (FadeParameter parameter, string newLevel)
	{
		if (newLevel == "Tutorial") 
		{
			PlayerPrefs.SetInt ("RemoveTutorial",1);
		}
		this.level = newLevel;
		string fade = (parameter == FadeParameter.FadeIn) ? "FadeIn" : "FadeOut"; 
		this.fadeController.StartFade (fade, FadeOutDone);
	}

	protected virtual void FadeOutDone()
	{
		OnFadeOutDone (null);
		if (this.level == null) 
		{
			return;		
		}
		SceneManager.LoadScene (this.level);
		this.level = null;
	}
	protected virtual void FadeInDone(){ OnFadeInDone (null); }

	public enum FadeParameter { FadeIn, FadeOut } 
}
