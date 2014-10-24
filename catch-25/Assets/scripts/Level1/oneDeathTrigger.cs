using UnityEngine;
using System.Collections;
using System;

public class oneDeathTrigger : MonoBehaviour {

	public static Action OnDeath = () => {};
	[SerializeField] private Environment environment;
	void Start()
	{
		switch(environment)
		{
			case Environment.FallingRock:
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

public enum Environment{
	FallingRock
}
