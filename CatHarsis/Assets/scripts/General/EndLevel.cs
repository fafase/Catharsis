using UnityEngine;
using System;
using System.Collections;

public class EndLevel : MonoBehaviour {
	public Action OnEnd = () => { };
	[SerializeField] private Skull skullManager;
	[SerializeField] private GameObject endMenu;

    private bool callOnce = false;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") && callOnce == false) 
		{
            callOnce = true;
			OnEnd();
			skullManager.SetSkullCloseNoCollider();
		}
	}
}
