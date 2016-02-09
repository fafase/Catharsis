using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiControllerLevelMap : UIController 
{
	[SerializeField] private Text currencyPlayer = null;
	[SerializeField] private Text levelPrice = null;
	[SerializeField] private GameObject levelCheckout = null;
	[SerializeField] private Button buttonYes = null;

	public LevelData CurrentLeveData { get; private set;}

	void Awake () 
	{
		this.levelCheckout.SetActive (false);
		this.fadeController.StartFade ("FadeIn",null);
		int playerCurrency = PlayerPrefs.GetInt ("Coin",0);
		this.currencyPlayer.text = playerCurrency.ToString ();
	}

	public void SetLevelPrice(LevelData levelData)
	{
		this.CurrentLeveData = levelData;
		this.levelCheckout.SetActive (true);
		this.levelPrice.text = levelData.Price.ToString ();
		int playerCurrency = PlayerPrefs.GetInt ("Coin",0);

		this.buttonYes.interactable = true;
		Image image = this.buttonYes.GetComponent<Image> ();
		image.color = Color.white;
		if (levelData.Price > playerCurrency) 
		{
			this.buttonYes.interactable = false;
			Color col = Color.white;
			col.a = 0.5f;
			image.color = col;
		}
	}

	public void SetLoadLevel()
	{
		if (this.CurrentLeveData == null) 
		{
			return;
		}
		int level = this.CurrentLeveData.Level;
		this.fadeController.StartFade ("FadeOut", ()=> { SceneManager.LoadScene("Level" + level.ToString());} );
	}
}
