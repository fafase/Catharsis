using UnityEngine;
using System.Collections;
using System;

public class CatHealth : MonoBehaviour {

	//public event Action OnLivesNull = () => {};
	public event Action OnChangeLives = () => {};
	[SerializeField] private int lives = 9;
	
	public int Lives{
		get{return lives;}
	}
	public int DecreaseHealth()
	{
		lives -= 1;
        int life = lives;
        if (lives < 0)
        {
            lives = 0;
            //OnLivesNull();
        }
        else 
        {
            OnChangeLives();
        }
        return life;
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
