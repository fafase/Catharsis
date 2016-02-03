using UnityEngine;
using System.Collections;
using System;

public class Test : StateMachine 
{
	protected GameObject[] GetOverLap(float radius, int layer, Func<Vector3, float,int,Collider[]> action){
		GameObject[] gameObjects = null;
		Collider[] colliders = action(transform.position, radius, layer);
		gameObjects = new GameObject[colliders.Length];
		for(int i = 0; i < colliders.Length; i++){
			gameObjects[i] = colliders[i].gameObject;
		}
		return gameObjects;
	}

	void Start(){
		GetOverLap (10,2,Physics.OverlapSphere);
	}
}
