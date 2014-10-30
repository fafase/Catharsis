using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

	public event Action<float> OnMovementCall= (f) => { };
	public event Action OnJumpCall = () => { };
    public event Action OnPause = () => { };
    public event Action OnRestart = () => { };
    public event Action OnPress = () => { };

	void Update () 
	{
        if (Input.anyKeyDown)
        {
            OnPress();  
        }

		float horizontal = Input.GetAxis ("Horizontal");
		OnMovementCall(horizontal);
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) 
		{
			OnJumpCall();
		}
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRestart();
        }
	}
}
