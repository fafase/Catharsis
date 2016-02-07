using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour 
{
    [SerializeField] private Animator anim = null;


    public void SetSkullOpen(bool value)
    {
        this.anim.SetBool("opening", value);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

        }
    }
}
