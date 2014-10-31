using UnityEngine;
using System;

public abstract class DeathController : MonoBehaviour 
{
	public static event Action<bool> OnDeath = (bool newClone) => {};

	protected void OnDeathCall(bool value)
	{
		OnDeath (value);
	}

	void OnDestroy() 
	{
		OnDeath = null;
	}
}
