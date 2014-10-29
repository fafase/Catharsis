﻿using UnityEngine;
using System.Collections;

public class CatMove : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float jumpForce = 500f;
    [SerializeField]
    private Animator anim;

    private bool facingRight = true;
    private bool grounded = false;
    private float movement = 0f;
    private bool isAlive = true;

    void Start()
    {
        DeathTrigger.OnDeath += ResetOnDeath;
    }
    void FixedUpdate()
    {
        if (isAlive == false)
        {
            return;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down,0.5f, whatIsGround);
        grounded = (hit.collider != null) ? true:false;
        anim.SetBool(Utility.ANIM_GROUND, grounded);
        if (grounded)
        {
            transform.up = hit.normal;
            anim.SetFloat(Utility.ANIM_SPEED, Mathf.Abs(rigidbody2D.velocity.x));
            anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        }
        else
        {
            anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
            anim.SetFloat(Utility.ANIM_VSPEED, rigidbody2D.velocity.y);
        }
        
        rigidbody2D.velocity = new Vector2(movement * maxSpeed, rigidbody2D.velocity.y);
    }

    public void Move(float move)
    {
        movement = move;
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Jump()
    {
        if (!grounded)
        {
            return;
        }
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void ResetOnDeath(bool newClone)
    {
        anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
        anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        anim.SetBool(Utility.ANIM_IS_DYING, true);
        isAlive = false;
    }

    public void ResetToPlay() 
    {
        isAlive = true;
        anim.SetBool(Utility.ANIM_IS_DYING, false);
    }
}
