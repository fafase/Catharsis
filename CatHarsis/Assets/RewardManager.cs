using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour 
{
	[SerializeField] private Text lifeText = null;
	[SerializeField] private Text coinText = null;
	[SerializeField] private Text totalText = null;
	[SerializeField] private AudioClip coinAudio = null;
	[SerializeField] private AudioSource audioSource = null;

	private void Awake()
	{
		this.lifeText.text = "0";
		this.coinText.text = "0";
		this.totalText.text = "0";
		this.audioSource = this.gameObject.AddComponent<AudioSource> ();
		this.audioSource.clip = this.coinAudio;
	}

	public void StartReward(IInventory inventory)  
	{
		int coinAmount = inventory.CoinAmount;
		int lives = inventory.LifeAmount;
		int total = coinAmount * (lives / 2);
		int totalCoin = PlayerPrefs.GetInt ("TotalCoin",0);
		total += totalCoin;
		PlayerPrefs.SetInt ("TotalCoin", total); 
		StartCoroutine (SetValueCoroutine(coinAmount, lives, total));
	}

	private IEnumerator SetValueCoroutine(int lives, int coins, int total)
	{
		yield return new WaitForSeconds (0.5f);
		for (int i = 0; i <= coins; i++) 
		{
			this.coinText.text = i.ToString ();
			this.audioSource.Play ();
			yield return new WaitForSeconds (0.1f);
		}
		yield return new WaitForSeconds (0.5f);
		for (int i = 0; i <= lives; i++) 
		{
			this.lifeText.text = i.ToString ();
			this.audioSource.Play ();
			yield return new WaitForSeconds (0.1f);
		}
		yield return new WaitForSeconds (0.5f);
		this.totalText.text = total.ToString ();
		this.audioSource.Play ();
	}
}
