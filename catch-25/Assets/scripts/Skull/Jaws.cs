using UnityEngine;
using System.Collections;

public class Jaws : MonoBehaviour {
	
	// Update is called once per frame
	void OnColliderEnter2D (Collision2D col) 
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			
		}
	}
}
