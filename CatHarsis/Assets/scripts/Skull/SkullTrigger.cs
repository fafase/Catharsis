using UnityEngine;
using System.Collections;

public class SkullTrigger : MonoBehaviour 
{
	[SerializeField] private Skull skullManager = null;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			skullManager.SetSkullOpen(true);
		}
	}
}
