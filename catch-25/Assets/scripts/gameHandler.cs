using UnityEngine;
using System.Collections;


/*	GAMEHANDLER
 *	This class is intended to handle all common values and actions that persist through levels.
 *	It should be attached to the mainCamera!
 *
 *	Features included are, for example:
 *	- Death counter
 *	- Death handler

 */

public class gameHandler : MonoBehaviour 
{
	private static gameHandler instance;
	public static gameHandler Instance{
		get
		{
			return instance;
		}
	}

	// Use this for initialization
	void Start () 
	{
		if(instance == null)
		{
			instance = this;
		}
	}
}

