using UnityEngine;
using System.Collections;

public class Flower : DeathTrigger 
{
	[SerializeField] private Animator anim;
	[SerializeField] private BoxCollider2D colliderBox;
	[SerializeField] private GameObject particles;
	protected override void CollisionCall (Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			OnDeathCall (true);
			particles.SetActive(false);
			colliderBox.enabled = false;
			anim.SetBool("retract", true);
		}
	}
}
