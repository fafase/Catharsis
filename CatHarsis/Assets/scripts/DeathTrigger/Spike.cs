using UnityEngine;
using System.Collections;

public class Spike : DeathCollider 
{

	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			OnDeathCall();
			AudioManager.Instance.PlayAudio("spike_impale",1.0f,1.0f);
		}
	}
}
