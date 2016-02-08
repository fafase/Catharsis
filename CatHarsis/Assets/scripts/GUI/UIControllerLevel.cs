using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIControllerLevel : UIController 
{
	[SerializeField] private GuiScore guiScore = null;
	private int finalCoin = 0;
	public void SetUISoul (int soulAmount)
	{
		this.guiScore.UpdateSoulGUIText (soulAmount);
	}
	protected override void FadeInDone()
	{
		OnFadeInDone (null);
	}

	public void ButtonActionHome()
	{
		this.fadeController.StartFade ("FadeOut", ButtonActionHomeCallback);
	}

	private void ButtonActionHomeCallback()
	{
		PlayerPrefs.SetInt ("Coin", this.finalCoin);
		SceneManager.LoadScene ("LevelMap");
	}
}
