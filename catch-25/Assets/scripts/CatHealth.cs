using UnityEngine;
using System.Collections;
using System;

public class CatHealth : MonoBehaviour {

	public event Action OnDeath = () => {};

	[SerializeField] private int lives = 9;

	public int Lives {
		get{
			return lives;
		}
		set
		{
			lives = value;
			if(lives <= 0)
			{
				OnDeath();
			}
		}
	}
}
