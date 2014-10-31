using UnityEngine;
using System.Collections;

public class Spike : DeathCollider 
{

	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			OnDeathCall(true);
			AudioManager.Instance.PlayAudio(Utility.SOUND_SPIKE_IMPALE,1.0f,1.0f);
		}
	}
}
