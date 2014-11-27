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


    public event Action OnJump = () => { };
    public event Action<float> OnMovement = (float f) => { };

    Rect control, topScreen;
    float width, height;
	void Awake () 
    {
        width = Screen.width;
        height = Screen.height;

        control = new Rect(0, height / 2f, width / 2f, height / 2f);
        float topMargin = height / 5f;
        topScreen = new Rect(0, 0 ,width, topMargin );

        InputManager.Instance.OnTouch += CheckInput;
	}
	
	void CheckInput(Vector3 position) 
    {
        Vector3 pos = position;
        float y = height - position.y;
        pos.y = y;
        if (control.Contains(pos) || topScreen.Contains(pos))
        {
            return;
        }
        OnJump();
	}

    public void SliderValue(float value) 
    {
        OnMovement(value);
    }
    void OnDestroy() 
    {
        OnMovement = null;
        OnJump = null;
    }
}
