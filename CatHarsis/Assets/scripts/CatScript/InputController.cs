using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// InputController is attached to the GameManager
/// Purpose is to link InputManager with classes needing input
/// CatController for cat movements
/// InGameGUI for GUI buttons
/// </summary>
public class InputController : Singleton<InputController> 
{

    [SerializeField]
    private CatController catController;
	
	public event EventHandler<EventArgs> RaiseJump;
	protected void OnJump(EventArgs arg)
	{
		if (RaiseJump != null) 
		{
			RaiseJump(this, arg);		
		}
	}
    public event Action<Vector3> RaiseMovement = (f) => { };

#if UNITY_EDITOR
	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			OnJump(null);
		}
	}
#endif

	private void OnDestroy() 
	{
		RaiseMovement = null;
		RaiseJump = null;
	}

	public void Jump() 
    {
		OnJump (null);
	}
}
