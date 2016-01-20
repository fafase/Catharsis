using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(InputManager))]
public class InputController : Singleton<InputController> 
{
	private InputManager inputManager = null;

	private void Awake()
	{
		this.inputManager = this.gameObject.GetComponent<InputManager> ();
	}

	public void RegisterSingleTap(IInputListener inputListener)
	{
		this.inputManager.OnSingleTap += inputListener.HandleSingleTap;
	}
	public void UnregisterSingleTap(IInputListener inputListener)
	{
		this.inputManager.OnSingleTap -= inputListener.HandleSingleTap;
	}
	public void RegisterDoubleTap(IInputListener inputListener)
	{
		this.inputManager.OnDoubleTap += inputListener.HandlerDoubleTap;
	}
	public void UnregisterDoubleTap(IInputListener inputListener)
	{
		this.inputManager.OnDoubleTap -= inputListener.HandlerDoubleTap;
	}	
	public void Register(IInputListener inputListener)
	{
		this.inputManager.OnSingleTap += inputListener.HandleSingleTap;
		this.inputManager.OnDoubleTap += inputListener.HandlerDoubleTap;
	}
	public void Unregister(IInputListener inputListener)
	{
		this.inputManager.OnSingleTap -= inputListener.HandleSingleTap;
		this.inputManager.OnDoubleTap -= inputListener.HandlerDoubleTap;
	}
	void OnDestroy()
	{
		this.inputManager = null;
	}
}

public interface IInputListener
{
	void HandleSingleTap(Vector3 vec);
	void HandlerDoubleTap();
}
