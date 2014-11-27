using UnityEngine;
using System.Collections;

public class SkullTrigger : MonoBehaviour {
	[SerializeField] private Skull skullManager;
	[SerializeField] private LayerMask layer;
	/*void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			skullManager.SetSkullOpen(true);
		}
	}*/

	void Update()
	{
		Collider2D [] cols = Physics2D.OverlapCircleAll (transform.position, 1f,layer);
		if (cols.Length == 0) 
		{
			return;
		}
		skullManager.SetSkullOpen(true);
	}
}
