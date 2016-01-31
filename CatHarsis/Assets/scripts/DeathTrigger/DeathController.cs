using UnityEngine;
using System;

public abstract class DeathController : MonoBehaviour 
{
	public static EventHandler<CatDeathEventArg> RaiseDeath;
	protected void OnDeath(CatDeathEventArg arg)
	{
		if (RaiseDeath != null) {
			RaiseDeath(this, arg);
		}
	}
	[SerializeField] private CatDeath catDeath;
	protected void OnDeathCall()
	{
		if (RaiseDeath != null) {
			RaiseDeath (this, new CatDeathEventArg (this.catDeath));
		}
	}

	void OnDestroy() 
	{
		RaiseDeath = null;
	}
}

public class CatDeathEventArg:System.EventArgs
{
	public readonly CatDeath catDeath;
	public CatDeathEventArg(CatDeath catDeath){
		this.catDeath = catDeath;
	}
}
