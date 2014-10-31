using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {

    [SerializeField]
    private Animator anim;

	bool b = false;
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			b =!b;
			SetSkullOpen(b);
		}
	}
    public void SetSkullOpen(bool value)
    {
        anim.SetBool("opening", value);
    }
}
