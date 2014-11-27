using UnityEngine;
using System.Collections;
using System;

public class InputMobileController : Singleton<InputMobileController> 
{
    public event Action OnTapAny = () => { };
    public event Action<Vector3> OnTap = (Vector3 v) => { };

    private Action UpdateDelegate;

    void Start() 
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

    void Update() 
    {
        UpdateDelegate();
    }

    private void UpdateMobile() 
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        OnTapAny();
        Touch[] touches = Input.touches;
        foreach (Touch t in touches)
        {
            OnTap(t.position);
        }
    }

    private void UpdateEditor() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTap(Input.mousePosition);
        }
    }

    void OnDestroy() 
    {
        OnTap = null;
        OnTapAny = null;
    }
}
