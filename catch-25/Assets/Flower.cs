using UnityEngine;
using System.Collections;

public class Flower : DeathTrigger 
{
	[SerializeField] private Animator anim;
	[SerializeField] private BoxCollider2D colliderBox;
	[SerializeField] private GameObject particles;

	protected override void TriggerCall (Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			AudioManager.Instance.PlayAudio(Utility.SOUND_POISONED,1.0f,1.0f);
			OnDeathCall (true);
			particles.SetActive(false);
			colliderBox.enabled = false;
			anim.SetBool("retract", true);
		}
	}
}
