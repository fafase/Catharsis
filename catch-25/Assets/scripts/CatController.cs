using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour 
{
	[SerializeField] private catMove catMoveRef;
	[SerializeField] private CatHealth catHealth;

	// Use this for initialization
	void Start () 
	{
		InputManager.OnMovement += catMoveRef.Move;
		InputManager.OnJump += catMoveRef.Jump;
	}

	public void Death()
	{
		catHealth.Lives -= 1;
	}
}
