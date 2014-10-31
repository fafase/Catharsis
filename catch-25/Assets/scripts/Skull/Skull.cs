using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {

    [SerializeField]
    private Animator anim;
	[SerializeField] private Collider2D[] jaws;
	[SerializeField] private float timer;


	private bool b = false;
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			print ("Yep");
			b =!b;
			SetSkullOpen(b);
		}
	}
    public void SetSkullOpen(bool value)
    {
        anim.SetBool("opening", value);
		foreach (Collider2D col in jaws) 
		{
			col.enabled = !value;
		}
		if (value == true) 
		{
			StartCoroutine(CloseSkull());
		}
    }
				
	private IEnumerator CloseSkull()
	{
		float mTimer = timer;
		while (mTimer > 0) 
		{
			mTimer -= Time.deltaTime;
			yield return null;
		}
		SetSkullOpen (false);
	}
}
