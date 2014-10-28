using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

	public static event Action<float> OnMovementCall= (f) => { };
	public static event Action OnJumpCall = () => {};
	void Update () 
	{
		float horizontal = Input.GetAxis ("Horizontal");
		OnMovementCall(horizontal);
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			OnJumpCall();
		}
	}
}
