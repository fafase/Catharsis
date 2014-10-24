using UnityEngine;
using System.Collections;


/*	GAMEHANDLER
 *	This class is intended to handle all common values and actions that persist through levels.
 *	It should be attached to the mainCamera!
 *
 *	Features included are, for example:
 *	- Death counter
 *	- Death handler

 */

public class gameHandler : MonoBehaviour {
	
	public float deathCount;
	private float playerTimer = 0.6f;	 // Lock time for player after death.
	private float cameraTimer = 0.3f; 	// Lock time for camera after death (slightly shorter).
	public bool playerLock = false;
	public bool cameraLock = false;

	private Transform player;
	private Transform spawn;
	private GameObject gameInterface;
	private GameObject body;
	

	// Use this for initialization
	void Start () {
		Debug.Log ("Initialized gameHandler.");
		Debug.Log ("Set death count to 0.");
		deathCount = 0f;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		spawn = GameObject.FindGameObjectWithTag("Respawn").transform;
		gameInterface = GameObject.Find("GUI");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void handlePlayerDeath(bool leaveCorpse) {
		StartCoroutine(death(leaveCorpse));
	}

	IEnumerator death(bool leaveCorpse){
		if (leaveCorpse) {
			// Make body
			body = (GameObject)Instantiate (Resources.Load ("deadBody"), player.position, Quaternion.identity);
		}
		deathCount++;
		gameInterface.guiText.text = "Deaths: "+deathCount+"/9";
		// Lock entities
		playerLock = true;
		cameraLock = true;
		// Respawn player & release
		player.position = new Vector2 (spawn.position.x - 0.25f, spawn.position.y);
		yield return new WaitForSeconds(cameraTimer);
		cameraLock = false;
		yield return new WaitForSeconds(playerTimer - cameraTimer);
		playerLock = false;

	}

}
