using UnityEngine;
using System.Collections;

public class GasLeak : DeathTrigger 
{
	SpriteRenderer spriteRenderer = null;
	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
			Invoke("GasInvoke", 3f);

		}
	}
	void GasInvoke()
	{
		AudioManager.Instance.PlayAudio(Utility.SOUND_POISONED,1.0f,1.0f);
		spriteRenderer.color = Color.green;
		OnDeathCall(true);
	}
}
