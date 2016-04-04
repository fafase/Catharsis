using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class UTest : MonoBehaviour 
{
	IDictionary<string, Func<GameObject,Component>> dict = new Dictionary<string, Func<GameObject,Component>> ();

	void Start()
	{
		dict.Add ("Rigidbody",(obj)=>{ return obj.AddComponent<Rigidbody>() as Component; });

		dict ["Rigidbody"](this.gameObject);
	}
}
