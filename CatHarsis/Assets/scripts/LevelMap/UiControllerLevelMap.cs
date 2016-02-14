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
	[SerializeField] private Regeneration regeneration = null;
	public LevelData CurrentLeveData { get; private set;}

	void Awake () 
	{
		this.levelCheckout.SetActive (false);
		this.fadeController.StartFade ("FadeIn",null);
		int playerCurrency = PlayerPrefs.GetInt ("Coin",0);
		//this.currencyPlayer.text = playerCurrency.ToString ();
		this.regeneration.RaiseNewLife += HandleNewLife;
	}
	private void HandleNewLife(object sender, RegenerationEventArg arg)
	{
		if (this.levelCheckout.activeSelf == false) {
			return;
		}
		this.buttonYes.interactable = true;
		Image image = this.buttonYes.GetComponent<Image> ();
		image.color = Color.white;

		if (this.regeneration.LifeAmount < this.CurrentLeveData.Price) 
		{
			this.buttonYes.interactable = false;
			Color col = Color.white;
			col.a = 0.5f;
			image.color = col;
		}	
	}
	public void SetLevelPrice(LevelData levelData)
	{
		this.CurrentLeveData = levelData;
		this.levelCheckout.SetActive (true);

		this.buttonYes.interactable = true;
		Image image = this.buttonYes.GetComponent<Image> ();
		image.color = Color.white;

		if (this.regeneration.LifeAmount < this.CurrentLeveData.Price) 
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
