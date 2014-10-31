using UnityEngine;
using System.Collections;
using System;

public class DeathTrigger : MonoBehaviour {

	public static event Action<bool> OnDeath = (bool newClone) => {};
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
			case EnvironmentItem.DeathZone:
				OnCollision = DeathZoneCollision;
			break;
			case EnvironmentItem.GasPoison:
				OnCollision = GasLeakCollision;
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
            OnDeath(true);
            AudioManager.Instance.PlayAudio(Utility.SOUND_SQUISHED, 1.0f, 1.0f);
        }
        if (col.gameObject.CompareTag("DeadCat"))
        {
            col.gameObject.collider2D.enabled = false;  
			col.gameObject.rigidbody2D.isKinematic = true;
        }
    }
    void SpikeCollision(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            OnDeath(true);
            AudioManager.Instance.PlayAudio(Utility.SOUND_SPIKE_IMPALE,1.0f,1.0f);
        }
	}
	void GasLeakCollision(Collision2D col) 
	{
		if (col.gameObject.CompareTag("Player")) 
		{
			AudioManager.Instance.PlayAudio(Utility.SOUND_POISONED,1.0f,1.0f);
			col.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			OnDeath(true);
		}
	}
    void DeathZoneCollision(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Player")) 
        {
			OnDeath(false);
			AudioManager.Instance.PlayAudio(Utility.SOUND_SQUISHED,1.0f,1.0f);
        }
    }
    void OnDestroy() 
    {
        OnDeath = null;
    }
}

public enum EnvironmentItem
{
	FallingRock, Spikes, DeathZone, GasPoison
}


