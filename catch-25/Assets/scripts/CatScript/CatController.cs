using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour 
{
	[SerializeField] private catMove catMoveRef;
	[SerializeField] private CatHealth catHealth;
	[SerializeField] private GameObject catPrefab;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () 
	{
		InputManager.OnMovement += catMoveRef.Move;
		InputManager.OnJump += catMoveRef.Jump;
		oneDeathTrigger.OnDeath += DeadCatClone;
		startPosition = transform.position;
	}

	private void DeadCatClone()
	{
		catHealth.DecreaseHealth ();
		Instantiate (catPrefab, transform.position, Quaternion.identity);
		transform.position = startPosition;
	} 
}
