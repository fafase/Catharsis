using UnityEngine;
using System.Collections;
using System;

public abstract class DeathTrigger : DeathController 
{
	protected abstract void TriggerCall (Collider2D col);

	protected void OnTriggerEnter2D(Collider2D col)
	{
		TriggerCall(col);
	}
}



