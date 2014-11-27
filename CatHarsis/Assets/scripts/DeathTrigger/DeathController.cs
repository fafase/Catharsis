using UnityEngine;
using System;

public abstract class DeathController : MonoBehaviour 
{
	public static event Action<CatDeath> OnDeath = (CatDeath catDeat) => {};
	[SerializeField] private CatDeath catDeath;
	protected void OnDeathCall()
	{
		print (catDeath);
		OnDeath (catDeath);
	}

	void OnDestroy() 
	{
		OnDeath = null;
	}
}
