using System;
using UnityEngine;

/// <summary>
/// InputManager is attached to the GameManager
/// It detects input and dispatch event
/// </summary>
public class InputManager : Singleton<InputManager> 
{

#if UNITY_EDITOR
	public event Action<float> OnMovementCall= (f) => { };
#endif
    public event Action<Vector3> OnTouch = (Vector3 v) => { };

    private Action UpdateDelegate = () => { };
    
    void Awake() 
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UpdateDelegate = UpdateMobile;
        }
        else 
        {
            UpdateDelegate = UpdateEditor;
        }
    }
	void Update () 
	{
        UpdateDelegate();        
	}

    void UpdateMobile() 
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        foreach (Touch touch in Input.touches)
        {
            OnTouch(touch.position);
        }
        return;
    }

    void UpdateEditor() 
    {
        Vector3 position = Input.mousePosition;

        float horizontal = Input.GetAxis("Horizontal");
        OnMovementCall(horizontal);
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnTouch(position);
        } 
    }

    void OnDestroy()
    {
        OnTouch = null;
#if UNITY_EDITOR
        OnMovementCall = null;
#endif
    }
}
