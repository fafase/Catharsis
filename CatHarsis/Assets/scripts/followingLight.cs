using UnityEngine;
using System.Collections;

public class followingLight : MonoBehaviour
{
	private Transform player;		// Reference to the player's transform.
	
	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
			float targetX = player.position.x;
			float targetY = player.position.y;
			transform.position = new Vector3 (targetX, targetY, transform.position.z);
	}


}
