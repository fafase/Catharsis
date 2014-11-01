using UnityEngine;
using System.Collections;

public class GasLeak : DeathTrigger 
{
	SpriteRenderer spriteRenderer = null;
	[SerializeField] private Transform castPoint;
	[SerializeField]
	private LayerMask layer;
    private bool oneHitOnly = false;

	[SerializeField] ParticleSystem particles;

	protected override void TriggerCall (Collider2D col)
	{
		if (col.gameObject.CompareTag("Player") && oneHitOnly == false) 
		{
            oneHitOnly = true;
            col.GetComponent<CatController>().PoisonCat();
			AudioManager.Instance.PlayAudio(Utility.SOUND_POISONED,1.0f,1.0f);
		}
	}

	void Update()
	{
		Collider2D [] cols = Physics2D.OverlapCircleAll (castPoint.position, 0.5f,layer);
		if (cols.Length == 0) 
		{
            oneHitOnly = false;
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
