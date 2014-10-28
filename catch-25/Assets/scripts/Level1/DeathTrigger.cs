using UnityEngine;
using System.Collections;
using System;

public class DeathTrigger : MonoBehaviour {

	public static event Action OnDeath = () => {};
    private Action<Collision2D> OnCollision = (Collision2D) => { };
	[SerializeField] private EnvironmentItem environment;
	void Start()
	{
		switch(environment)
		{
			case EnvironmentItem.FallingRock:
                OnCollision = FallingRockCollision;
			    break;
            case EnvironmentItem.Spikes:
                OnCollision = SpikeCollision;
                break;
		}
	}
	void OnCollisionEnter2D(Collision2D col)
	{
        OnCollision(col);
	}
    void FallingRockCollision(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && rigidbody2D.isKinematic == false)
        {
            OnDeath();
        }
        if (col.gameObject.CompareTag("DeadCat"))
        {
            col.gameObject.collider2D.enabled = false;
        }
    }
    void SpikeCollision(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            OnDeath();
        }
    }
    void OnDestroy() 
    {
        OnDeath = null;
    }
}

public enum EnvironmentItem
{
	FallingRock, Spikes
}


