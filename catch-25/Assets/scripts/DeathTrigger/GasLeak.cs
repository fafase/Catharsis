using UnityEngine;
using System.Collections;

public class GasLeak : DeathTrigger 
{
	SpriteRenderer spriteRenderer = null;
	[SerializeField] private Transform castPoint;
	[SerializeField]
	private LayerMask layer;
	[SerializeField] ParticleSystem particles;
	protected override void TriggerCall (Collider2D col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			print ("Call");
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

	void Update()
	{
		Collider2D [] cols = Physics2D.OverlapCircleAll (castPoint.position, 0.5f,layer);
		if (cols.Length == 0) 
		{
			particles.startLifetime = 3f;
			return;
		}
		collider2D.enabled = false;
		particles.startLifetime = 0.1f;
	}
}
