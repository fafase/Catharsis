using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {

    [SerializeField]
    private Animator anim;

    public void SetSkullOpen(bool value)
    {
        anim.SetBool("opening", value);
    }
}
