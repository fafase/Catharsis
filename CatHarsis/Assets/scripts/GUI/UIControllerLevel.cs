using UnityEngine;
using System.Collections;

public class UIControllerLevel : UIController 
{
	[SerializeField] private GuiScore guiScore = null;
	public void SetUISoul (int soulAmount)
	{
		this.guiScore.UpdateSoulGUIText (soulAmount);
	}
	protected override void FadeInDone()
	{
		OnFadeInDone (null);
	}
}
