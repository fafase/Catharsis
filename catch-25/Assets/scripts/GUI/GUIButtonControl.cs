using UnityEngine;
using System.Collections;
using System;

public class GUIButtonControl : MonoBehaviour {

    [SerializeField]
    private GUITexture[] btn;


    private Action[] actions = new Action[3];
    private Vector3 mousePosition = Vector3.zero;
    void Awake() 
    {
        InputManager inputManager = FindObjectOfType<InputManager>();
        inputManager.OnMousePointer += OnMousePointerEvent;
        inputManager.OnMouseDown += OnMousePressEvent;
        inputManager.OnMouseUp += OnMouseClickEvent;

        PauseHandler pauseHandler = FindObjectOfType<PauseHandler>();
        if (pauseHandler != null)
        {
            actions[0] = pauseHandler.ButtonWebSite;
            actions[1] = pauseHandler.ButtonFacebook;
            actions[2] = pauseHandler.ButtonQASurvey;
        }       
    }

    void OnMousePointerEvent(Vector3 position) 
    {
        if (mousePosition == position)
        {
            return;
        }
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].color = Color.white;
            if (btn[i].HitTest(position))
            {
                btn[i].color = Color.green;
            }
        }
    }
    void OnMousePressEvent(Vector3 position) 
    {
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].color = Color.white;
            if (btn[i].HitTest(position))
            {
                btn[i].color = Color.red;
            }
        }
    }
    void OnMouseClickEvent(Vector3 position)
    {
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].color = Color.white;
            if (btn[i].HitTest(position))
            {
                btn[i].color = Color.blue;
                actions[i]();
            }
        }
    }
}

