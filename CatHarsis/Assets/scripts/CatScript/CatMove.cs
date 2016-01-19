using UnityEngine;
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
		if (Mathf.Abs (this.target.x - this.transform.position.x) < 0.5f) 
		{
			this.target = this.transform.position;
			return;
		}
		this.rig.velocity = new Vector2(movement * maxSpeed, velY);
    }

    public void Move(Vector3 position)
    {
		float deltaX = position.x - transform.position.x; 
		movement = deltaX > 0 ? 1f: -1f;
		this.target = position;
		if (deltaX > 0 && !facingRight)
        {
            Flip();
        }
		else if (deltaX < 0 && facingRight)
        {
            Flip();
        }
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

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void ResetOnDeath()
    {
        anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
        anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        anim.SetBool(Utility.ANIM_IS_DYING, true);
    }

    public void ResetToPlay() 
    {
        anim.SetBool(Utility.ANIM_IS_DYING, false);
       // movement = 0f;
    }
}
