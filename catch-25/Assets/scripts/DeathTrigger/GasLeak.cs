using UnityEngine;
using System.Collections;

public class GasLeak : DeathTrigger 
{
	SpriteRenderer spriteRenderer = null;
	[SerializeField] private Transform castPoint;
	[SerializeField]
	private LayerMask layer;

	private Color greenify;
	private Color whiten;

	[SerializeField] ParticleSystem particles;

	protected override void TriggerCall (Collider2D col)
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			print ("Call");
			greenify = new Color(0.1f,0.7f,0.1f,1.0f);
			whiten = new Color(1.0f,1.0f,1.0f,1.0f);
			AudioManager.Instance.PlayAudio(Utility.SOUND_POISONED,1.0f,1.0f);
			spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.color = greenify;
			Invoke("GasInvoke", 3f);
		}
	}
	void GasInvoke()
	{
		spriteRenderer.color = whiten;
		OnDeathCall(true);
	}

	void Update()
	{
		Collider2D [] cols = Physics2D.OverlapCircleAll (castPoint.position, 0.5f,layer);
		if (cols.Length == 0) 
		{
			particles.startLifetime = 3f;
			particles.startSpeed = 1f;
			collider2D.enabled = true;
			return;
		}
		collider2D.enabled = false;
		particles.startLifetime = 0.3f;
		particles.startSpeed = 0.1f;
	}
}
