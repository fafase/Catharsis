using UnityEngine;
using System.Collections;

public class FallingRock : DeathTrigger 
{
	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player") && rigidbody2D.isKinematic == false) 
		{
			OnDeathCall (true);
			AudioManager.Instance.PlayAudio (Utility.SOUND_SQUISHED, 1.0f, 1.0f);
		}
		if (col.gameObject.CompareTag ("DeadCat")) 
		{
			col.gameObject.collider2D.enabled = false;  
			col.gameObject.rigidbody2D.isKinematic = true;
		}
	}
}
