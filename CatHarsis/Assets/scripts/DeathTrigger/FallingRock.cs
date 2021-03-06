﻿using UnityEngine;
using System.Collections;

public class FallingRock : DeathCollider
{
    private bool oneHitOnly = false;
	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player") && GetComponent<Rigidbody2D>().isKinematic == false && oneHitOnly == false) 
		{
            oneHitOnly = true;
			OnDeathCall ();
			AudioManager.Instance.PlayAudio ("squished", 1.0f, 1.0f);
		}
	}
}
