using UnityEngine;
using System;

public class TutorialController : MonoBehaviour
{
	public Action<Vector3> OnSingleTap;
	public Action OnDoubleTap;
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
	private void Update(){
		if(updateDelegate != null){ updateDelegate();}
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
			if (Time.time < tapTimer + this.tapThreshold)
			{
				if(OnDoubleTap != null) { OnDoubleTap(); }
 				tap = false;
				return;
			}
			tap = true;
			tapTimer = Time.time;
		}
		
		if (tap == true && Time.time > tapTimer + this.tapThreshold) 
		{
			tap = false;
			if(OnSingleTap != null) { OnSingleTap(Input.mousePosition); }
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