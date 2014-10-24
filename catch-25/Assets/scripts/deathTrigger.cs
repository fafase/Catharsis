using UnityEngine;
using System.Collections;

public class deathTrigger : MonoBehaviour {
	
	private GameObject mainCamera;

	// Use this for initialization
	void Awake () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void OnTriggerEnter2D(Collider2D colliderObject) {
		if (colliderObject.gameObject.tag == "Player") {
			// Handle player
			mainCamera.GetComponent<gameHandler>().handlePlayerDeath(true);
		}
	}
}
