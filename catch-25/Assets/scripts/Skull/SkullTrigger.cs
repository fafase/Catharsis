using UnityEngine;
using System.Collections;

public class SkullTrigger : MonoBehaviour {
	[SerializeField] private Skull skullManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			skullManager.SetSkullOpen(true);
		}
	}
}
