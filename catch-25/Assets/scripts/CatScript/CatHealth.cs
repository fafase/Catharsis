using UnityEngine;
using System.Collections;
using System;

public class CatHealth : MonoBehaviour {

	public event Action OnLivesNull = () => {};
	public event Action OnChangeLives = () => {};
	[SerializeField] private int lives = 9;
	
	public int Lives{
		get{return lives;}
	}
	public void DecreaseHealth()
	{
		lives -= 1;
		OnChangeLives ();
		if (lives < 0) 
		{
            lives = 0;
			OnLivesNull();
		}
	}
    public void IncreaseHealth() 
    {
        if (++lives > 9)
        {
            lives = 9;
        }
        OnChangeLives();
    }
}
