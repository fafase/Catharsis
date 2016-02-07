using UnityEngine;
using System.Collections;
using System;

public class UIController : MonoBehaviour
{
	[SerializeField] protected FadeController fadeController = null;
	protected string level = null;

	public EventHandler<EventArgs> RaiseFadeOutDone;
	public EventHandler<EventArgs> RaiseFadeInDone;

	protected void OnFadeInDone(EventArgs arg)
	{
		if (RaiseFadeInDone != null) 
		{
			RaiseFadeInDone(this, arg);
		}
	}

	protected void OnFadeOutDone(EventArgs arg)
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

	public virtual void SetLevel (string parameter, string newLevel)
	{
		this.level = newLevel;
		this.fadeController.StartFade (parameter, FadeOutDone);
	}

	protected virtual void FadeOutDone()
	{
		OnFadeOutDone (null);
		if (this.level == null) 
		{
			return;		
		}
		Application.LoadLevel (this.level);
		this.level = null;
	}
	protected virtual void FadeInDone(){ OnFadeInDone (null); }
}
