﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
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
    [SerializeField]
    private bool wallJump = false;
    [SerializeField]
    private PhysicsMaterial2D mat;
    private bool facingRight = true;
    private bool grounded = false;
    private float movement = 0f;
	private Rigidbody2D rig = null;
	private Vector3 target;

	private void Awake()
	{
		this.rig = GetComponent<Rigidbody2D> ();
		this.target = this.transform.position;
	}

    private void FixedUpdate()
    {
		float velY = this.rig.velocity.y;
		velY = (velY > 8.24f) ? 8f : velY;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.50f, whatIsGround);
        grounded = (hit.collider != null) ? true : false;       
        anim.SetBool(Utility.ANIM_GROUND, grounded);

        if (grounded)
        {
            mat.friction = 1f;
            float angle = Vector2.Angle(Vector2.up, hit.normal);
            transform.up = (angle > 30f) ? hit.normal : Vector2.up;
           
            anim.SetFloat(Utility.ANIM_SPEED, Mathf.Abs(this.rig.velocity.x));
        }
        else
        {
            mat.friction = 0f; 
            transform.up = Vector2.up;
            anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
            anim.SetFloat(Utility.ANIM_VSPEED, this.rig.velocity.y);
        }
		this.rig.velocity = new Vector2(movement * maxSpeed, velY);
    }

    public void Move(Vector3 position, bool overrideDirection = false)
    {
		float deltaX = position.x - transform.position.x; 
		float tempMove = deltaX > 0 ? 1f : -1f;
		if (overrideDirection == false) 
		{
			this.movement = (Mathf.Approximately (tempMove, this.movement) == true) ? 0f : tempMove;
		} else {
			this.movement = tempMove;
		}
		if (deltaX > 0 && !facingRight)
        {
            Flip();
        }
		else if (deltaX < 0 && facingRight)
        {
            Flip();
        }
    }
	public void StopMovement()
	{
		this.movement = 0;
	}
    public void Jump()
	{
        if (!grounded)
        {
            if (wallJump == false)
            {
                return;
            }
            float sideForce = -jumpForce * 2f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right * 0.5f, 0.5f, whatIsGround);
            if (hit.collider == null)
            {
                hit = Physics2D.Raycast(transform.position, Vector3.right * -0.5f, 0.5f, whatIsGround);
                if (hit.collider == null)
                {
                    return;
                }
                sideForce *= -1f;
            }
                 
            Vector2 force = new Vector2(sideForce, jumpForce * 1.1f);
            this.rig.AddForce(force);
            return;
        }
        this.rig.AddForce(new Vector2(0, jumpForce));
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void ResetOnDeath()
    {
		this.rig.velocity = Vector2.zero;
		this.movement = 0f;
        this.anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
        this.anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        this.anim.SetBool(Utility.ANIM_IS_DYING, true);
		this.facingRight = true;
		Vector3 theScale = transform.localScale;
		theScale.x = Mathf.Abs (theScale.x);
		transform.localScale = theScale;
    }

    public void ResetToPlay(Vector3 targetReset) 
    {
		//this.target = targetReset;
        anim.SetBool(Utility.ANIM_IS_DYING, false);
    }
}
