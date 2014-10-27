using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

	public static event Action<float> OnMovement = (f) => { };
	public static event Action OnJump = () => {};
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontal = Input.GetAxis ("Horizontal");
		OnMovement(horizontal);
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			OnJump();
		}
	}
}
