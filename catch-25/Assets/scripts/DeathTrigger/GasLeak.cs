using UnityEngine;
using System.Collections;

public class GasLeak : DeathTrigger 
{

	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			AudioManager.Instance.PlayAudio(Utility.SOUND_POISONED,1.0f,1.0f);
			col.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			OnDeathCall(true);
		}
	}
}
