using UnityEngine;
using System;
using System.Collections;

public class EndLevel : MonoBehaviour {
	public Action OnEnd = () => { };
	[SerializeField] private Skull skullManager;
	[SerializeField] private GameObject endMenu;
	CanvasGroup canvasGroup;


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			OnEnd();
			skullManager.SetSkullCloseNoCollider();
			endMenu.SetActive (true);
			canvasGroup = endMenu.GetComponent<CanvasGroup> ();
			canvasGroup.alpha = 0;
			StartCoroutine (FadeInEndScreen());
		}
	}

	private IEnumerator FadeInEndScreen ()
	{
		float timer = 0f;
		while (timer < 1f) 
		{
			timer += Time.deltaTime * 0.5f;
			canvasGroup.alpha = timer;
			yield return null;
		}
	}
}
