using UnityEngine;
using System.Collections;
using System;


public class UTest : MonoBehaviour 
{
	int seconds = 300;
	private int life = 5;
	public int currentLife  = 0;
	private DateTime target;
	void Start()
	{
		System.DateTime value = new System.DateTime (2016, 02, 12, 23, 15, 00);
		System.DateTime now = System.DateTime.Now;

		TimeSpan ts = now - value;
		int elapsed = (int)ts.TotalSeconds;

		int addLife = elapsed / seconds;
		currentLife += addLife;
		currentLife = (currentLife > life) ? life : currentLife;
		int remainingSec = seconds - (elapsed - (addLife * seconds));

		target = now.AddSeconds (remainingSec);
	}

	void Update()
	{
		if (life == currentLife) 
		{
			Debug.Log (currentLife.ToString()+":00");
			return;
		}
		TimeSpan ts = target.Subtract(DateTime.Now);
		Debug.Log (ts.Minutes.ToString()+":"+ts.Seconds.ToString("00"));

	}
}
