using UnityEngine;
using System.Collections;

public class oneDeathTrigger : MonoBehaviour {
	
	/*private Transform player;
	private Transform spawn;
	private GameObject gameInterface;
	private GameObject body;
	private GameObject mainCamera;
	private gameHandler gameHandler;
	
	private bool usedUp = false;

	// Use this for initialization
	void Awake () 
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		gameHandler gameHandler = mainCamera.GetComponent<gameHandler>();
		gameInterface = GameObject.Find("GUI");
	}

	
	void OnTriggerEnter2D(Collider2D colliderObject) {
		Debug.Log(gameObject.name +" collided with " +colliderObject.name);
		
		if (colliderObject.gameObject.tag == "Player") {
			if (!usedUp) {
				usedUp = true;
				// Handle player death
				mainCamera.GetComponent<gameHandler>().handlePlayerDeath(true);
			}
		}
		if (colliderObject.gameObject.name == "rockStopper") 
		{
			gameObject.collider2D.isTrigger = false;
			gameObject.transform.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}*/

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			print ("Call");
		}
	}
}
