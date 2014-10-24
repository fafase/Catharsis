using UnityEngine;
using System.Collections;

public class rockTrigger : MonoBehaviour {

	private bool rockFallen = false;
	[SerializeField] private Transform player;
	[SerializeField] private Rigidbody2D rockRigidbody;
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
		float distance = Vector2.Distance (transform.position, target.position);
		if(distance < range)
		{
			if (!rockFallen) 
			{
				rockRigidbody.isKinematic = false;
				rockFallen = true;
				target = rockRigidbody.transform;
				range = 0.2f;
			} 
			else 
			{
				rockRigidbody.isKinematic = true;
				Destroy (gameObject);
			}
		}
	}
}
