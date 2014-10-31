using UnityEngine;
using System.Collections;
using System;

public abstract class DeathTrigger : MonoBehaviour {

	public static event Action<bool> OnDeath = (bool newClone) => {};
	protected abstract void CollisionCall (Collision2D col);


	protected void OnDeathCall(bool value)
	{
		OnDeath (value);
	}
	protected void OnCollisionEnter2D(Collision2D col)
	{
        CollisionCall(col);
	}

    void OnDestroy() 
    {
        OnDeath = null;
    }
}



