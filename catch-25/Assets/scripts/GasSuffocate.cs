using UnityEngine;
using System.Collections;

public class GasSuffocate : MonoBehaviour {

	[SerializeField]
	private bool gasOn = true;
	private ParticleSystem gasParticles;
	private Transform checkPosition;
	private Transform player;
	private Transform deadcat;
	private float range;

	private bool isSuffocated = false;
	void Start()
	{
		player = GameObject.Find("Cat").transform;
		range = 1.5f;
		gasParticles = transform.parent.Find ("GasParticles").particleSystem;

		deadcat = GameObject.Find("DeadCat").transform;
		range = 1.5f;
		gasParticles = transform.parent.Find ("GasParticles").particleSystem;
	}

	// Object enters suffocate zone
	void OnTriggerEnter2D(Collider2D target) {
		if (target.gameObject.transform == player || target.gameObject.CompareTag("DeadCat")) {
			gasOn = false;
			gasParticles.startLifetime = 1.0f;
			gasParticles.startSpeed = 0.2f;
		}	
	}

	// Object leaves suffocate zone
		void OnTriggerExit2D(Collider2D target) {
		if (isSuffocated == true) 
		{
			return;
		}

		if (target.gameObject.transform == player || target.gameObject.transform == player) {
			gasOn = true;
			gasParticles.startLifetime = 3.0f;
			gasParticles.startSpeed = 1.0f;

		}	
		if (target.gameObject.transform == deadcat || target.gameObject.transform == deadcat) {
			gasOn = true;
			gasParticles.startLifetime = 3.0f;
			gasParticles.startSpeed = 1.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Audio distance volume
		float distance = Vector2.Distance(transform.position, player.position);
		transform.audio.volume = Mathf.Clamp(4f - distance, 0f, 0.4f);
	}
}
