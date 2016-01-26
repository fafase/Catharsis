using System;
using UnityEngine;

/// <summary>
/// InputManager is attached to the GameManager
/// It detects input and dispatch event
/// </summary>
public class InputManager : Singleton<InputManager> 
{
	
	public event Action<Vector3> OnSingleTap;
	public event Action OnDoubleTap;
	public event Action<Vector2, Vector2> OnSwipe;

	[Tooltip("Defines the maximum time between two taps to make it double tap")]
	[SerializeField]private float tapThreshold = 0.25f;

	private Action updateDelegate;
	private float tapTimer = 0.0f;
	private bool tap = false;
    
	private void Awake()
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		updateDelegate = UpdateEditor;
#elif UNITY_IOS || UNITY_ANDROID
		updateDelegate = UpdateMobile;
#endif
	}
	void Update () 
	{
		updateDelegate();        
	}

	private void OnDestroy()
	{
		OnSingleTap = null;
		OnDoubleTap = null;
	}
	#if UNITY_EDITOR || UNITY_STANDALONE
	private void UpdateEditor() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (Time.time < this.tapTimer + this.tapThreshold)
			{
				if(OnDoubleTap != null)	{ OnDoubleTap(); } 
				this.tap = false;
				return;
			}
			this.tap = true;
			this.tapTimer = Time.time;
		}
		if (tap == true && Time.time > tapTimer + .3f) 
		{
			tap = false;
			if(OnSingleTap != null) { OnSingleTap(Input.mousePosition);}
		}	
	}
	#elif UNITY_IOS || UNITY_ANDROID
	private void UpdateMobile () 
	{
		for (var i = 0; i < Input.touchCount; ++i)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Began) 
			{
				if(Input.GetTouch(i).tapCount == 2)
				{
					OnDoubleTap();
				}
				if(Input.GetTouch(i).tapCount == 1)
				{
					OnSingleTap(Input.GetTouch(i).position);
				}
			}
		}
	}
	#endif
}
