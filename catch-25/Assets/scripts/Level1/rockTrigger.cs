using UnityEngine;
using System.Collections;

public class RockTrigger : MonoBehaviour {

	private bool rockFallen = false;
	[SerializeField] private Transform player;
	[SerializeField] private Rigidbody2D rockRigidbody;
    [SerializeField] private Transform checkPosition;
	private Transform target;
	private float range;
	void Start () 
	{
		rockRigidbody.isKinematic = true;
		target = player;
		range = 1f;
	}
	void Update()
	{
		float distance = Vector2.Distance (checkPosition.position, target.position);
		if(distance < range)
		{
			if (!rockFallen) 
			{
				rockRigidbody.isKinematic = false;
				rockFallen = true;
				target = rockRigidbody.transform;
				range = 0.2f;
                AudioManager.Instance.PlayAudio(Utility.SOUND_FALLING_ROCK);
			} 
			else 
			{
				rockRigidbody.isKinematic = true;
				Destroy (this);
			}
		}
	}
}
