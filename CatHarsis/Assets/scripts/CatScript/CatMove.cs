using UnityEngine;
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
    [SerializeField]
    private bool wallJump = false;
    [SerializeField]
    private PhysicsMaterial2D mat;
    private bool facingRight = true;
    private bool grounded = false;
    private float movement = 0f;

    void FixedUpdate()
    {
		float velY = rigidbody2D.velocity.y;
		velY = (velY > 8.24f) ? 8f : velY;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.50f, whatIsGround);
        grounded = (hit.collider != null) ? true : false;       
        anim.SetBool(Utility.ANIM_GROUND, grounded);

        if (grounded)
        {
            mat.friction = 1f;
            float angle = Vector2.Angle(Vector2.up, hit.normal);
            transform.up = (angle > 30f) ? hit.normal : Vector2.up;
           
            anim.SetFloat(Utility.ANIM_SPEED, Mathf.Abs(rigidbody2D.velocity.x));
            anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        }
        else
        {
            mat.friction = 0f; 
            transform.up = Vector2.up;
            anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
            anim.SetFloat(Utility.ANIM_VSPEED, rigidbody2D.velocity.y);
        }
        rigidbody2D.velocity = new Vector2(movement * maxSpeed, velY);
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
            rigidbody2D.AddForce(force);
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
    public void ResetOnDeath()
    {
        anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
        anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        anim.SetBool(Utility.ANIM_IS_DYING, true);
    }

    public void ResetToPlay() 
    {
        anim.SetBool(Utility.ANIM_IS_DYING, false);
        movement = 0f;
    }
}
