using UnityEngine;
using System.Collections;

public class SetRespawn : MonoBehaviour
{
	[SerializeField]
	private Transform medusa;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private bool activeJelly = true;

	void Start()
	{
		medusa = GameObject.Find("Jelly").transform;
		player = GameObject.Find("Cat");
	}

	void OnTriggerEnter2D(Collider2D target) 
	{
		if (target.gameObject == player && activeJelly) 
		{
			AudioManager.Instance.PlayAudio(Utility.SOUND_RESPAWN,0.7f,0.7f);
			medusa.position = new Vector3 (transform.position.x, transform.position.y, 0);
			activeJelly = false;
		}	
	}
}
