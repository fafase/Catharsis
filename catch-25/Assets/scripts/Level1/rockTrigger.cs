using UnityEngine;
using System.Collections;

public class rockTrigger : MonoBehaviour {

	private bool rockFallen = false;
	private GameObject player;
	private GameObject rock;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rock = GameObject.Find("fallingRock");
		rock.rigidbody2D.gravityScale=0;
	}

	void OnTriggerEnter2D(Collider2D player){
		if (!rockFallen) {
			rock.rigidbody2D.gravityScale = 1;
			rockFallen = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
