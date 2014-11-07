using UnityEngine;
using System.Collections;
using System;

public class InputManager : MonoBehaviour {

	public event Action<float> OnMovementCall= (f) => { };
	public event Action OnJumpCall = () => { };
    public event Action OnPause = () => { };
    public event Action OnRestart = () => { };
    public event Action OnPress = () => { };
    public event Action<Vector3> OnMousePointer = (Vector3 v) => { };
    public event Action<Vector3>OnMouseDown = (Vector3 v) =>{};
    public event Action<Vector3> OnMouseUp = (Vector3 v) => { };
    private bool isJoystick = false;

    void Awake() 
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            isJoystick = true;
        }
    }
	void Update () 
	{   
        Vector3 position = Input.mousePosition;
        OnMousePointer(position);
        if (Input.GetMouseButton(0))
        {
            OnMouseDown(position);
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp(position);
        }

        if (Input.anyKeyDown)
        {
            OnPress();  
        }

		float horizontal = Input.GetAxis ("Horizontal");
		OnMovementCall(horizontal);
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Space)) 
		{
			OnJumpCall ();
		} 
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRestart();
        }

        if (isJoystick == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            OnJumpCall();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            OnPause();
        }
	}
}
