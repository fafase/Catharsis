using UnityEngine;
using System.Collections;

public class Flower : DeathTrigger 
{

	protected abstract void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			
		}
	}
}
