using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

	public static event Action<float> OnMovementCall= (f) => { };
	public static event Action OnJumpCall = () => { };
    public static event Action OnPause = () => { };
    public static event Action OnPress = () => { };
	void Update () 
	{
        if (Input.anyKeyDown)
        {
            OnPress();
        }
		float horizontal = Input.GetAxis ("Horizontal");
		OnMovementCall(horizontal);
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			OnJumpCall();
		}
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPause();
        }
	}
}
