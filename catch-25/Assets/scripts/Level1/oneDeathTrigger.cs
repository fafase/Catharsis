using UnityEngine;
using System.Collections;
using System;

public class oneDeathTrigger : MonoBehaviour {

	public static Action OnDeath = () => {};
	[SerializeField] private EnvironmentItem environment;
	void Start()
	{
		switch(environment)
		{
			case EnvironmentItem.FallingRock:
			break;
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player") && rigidbody2D.isKinematic == false) 
		{
			OnDeath();
			//OnCollide(col);
		}
		if (col.gameObject.CompareTag ("DeadCat"))
		{
			col.gameObject.collider2D.enabled = false;
		}
	}
}

public enum EnvironmentItem{
	FallingRock
}
