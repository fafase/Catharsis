using UnityEngine;
using System.Collections;
using System;

public class Test : StateMachine 
{
	public Transform player = null;
	private float aimDistance = 10f;
	private float shootDistance = 5f;
	void Awake()
	{
		InitializeStateMachine<State> (State.Wait, true);
		AddTransitionsToState (State.Wait, new System.Enum[]{ State.Aim ,State.Die });
		AddTransitionsToState (State.Aim, new System.Enum[]{ State.Wait ,State.Shoot, State.Die });
		AddTransitionsToState (State.Shoot, new System.Enum[]{ State.Aim ,State.Die });
		AddTransitionsToState (State.Die, new System.Enum[]{ State.Wait });
		//this.player = GameObject.Find ("Player").transform;
	}
	private void EnterWait(Enum previous)
	{
		// init some stuff
		// Enable renderer
	}
	private void UpdateWait()
	{
		if(Vector3.Distance (this.player.position, this.transform.position) < this.aimDistance)
	 	{
			ChangeCurrentState(State.Aim);
		}
	}

	private void UpdateAim()
	{
		Quaternion targetRotation = Quaternion.LookRotation (this.player.position - this.transform.position);
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation, targetRotation, Time.deltaTime);
		if(Vector3.Distance (this.player.position, this.transform.position) < this.shootDistance)
		{
			ChangeCurrentState(State.Shoot);
		}
	}
	private float shootTimer = 1f;
	private float timer = 0f;
	private void UpdateShoot()
	{
		if (this.timer > this.shootTimer) {
			Shoot ();
			this.timer = 0f;
		}
		this.timer += Time.deltaTime;
		if(Vector3.Distance (this.player.position, this.transform.position) > this.shootDistance)
		{
			ChangeCurrentState(State.Aim);
		}
	}
	private void EnterDie(Enum previous)
	{
		// Disable renderer

	}
	private void Shoot(){ Debug.Log(" Pow "); }
	enum State{ Wait, Aim, Shoot, Die }
}
