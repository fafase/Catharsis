using UnityEngine;
using System.Collections;

public abstract class DeathCollider : DeathController {
	

	protected abstract void CollisionCall (Collision2D col);

	protected void OnCollisionEnter2D(Collision2D col)
	{
		CollisionCall(col);
	}
}
