using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {
	[SerializeField] private GameHandler gameHandler;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			gameHandler.RequestStateHandler(Utility.GAME_STATE_GAMEWON);
		}
	}
}
