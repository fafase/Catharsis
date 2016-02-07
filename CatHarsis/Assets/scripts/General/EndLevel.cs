using UnityEngine;
using System;
using System.Collections;

public class EndLevel : MonoBehaviour {
	public EventHandler<EventArgs> RaiseEndLevel;
	protected void OnEndLevel(EventArgs arg)
	{
		if (RaiseEndLevel != null) 
		{
			RaiseEndLevel(this, arg);		
		}
	}

	[SerializeField] private Skull skullManager;
	[SerializeField] private GameObject endMenu;

    private bool callOnce = false;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") && callOnce == false) 
		{
            callOnce = true;
			OnEndLevel(null);
			skullManager.SetSkullOpen(false);
		}
	}
}
