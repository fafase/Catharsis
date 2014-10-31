using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {

    [SerializeField]
    private Animator anim;
	[SerializeField] private Collider2D[] jaws;
	[SerializeField] private float timer;
	private float setTimer;
	private bool isOpen = false;

	void Start()
	{
		setTimer = timer;
	}
	public bool IsOpen
	{
		get{return isOpen;}
	}
	public void ResetTimer()
	{
		mTimer = setTimer;
	}
    public void SetSkullOpen(bool value)
    {
		if (value == true && isOpen)
		{
			ResetTimer();
			return;
		}
        anim.SetBool("opening", value);
		foreach (Collider2D col in jaws) 
		{
			col.enabled = !value;
		}
		if (value == true && !isOpen) 
		{
			StartCoroutine (CloseSkull ());
			isOpen = value;
		}
    }

	private float mTimer;			
	private IEnumerator CloseSkull()
	{
		mTimer = timer;
		while (mTimer > 0) 
		{
			mTimer -= Time.deltaTime;
			yield return null;
		}
		SetSkullOpen (false);
	}
	public void SetSkullCloseNoCollider()
	{
		StopAllCoroutines ();
		isOpen = false;
		anim.SetBool("opening", false);
	}
}
