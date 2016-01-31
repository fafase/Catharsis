using UnityEngine;
using System.Collections;

public class DeathZone : DeathCollider
{
	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			OnDeathCall();
			AudioManager.Instance.PlayAudio("squished",1.0f,1.0f);
		}
	}
}
