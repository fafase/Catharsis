using UnityEngine;
using System.Collections;

public class CatMovement : MonoBehaviour {

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
	private float groundRadius = 0.2f;
	private float movement = 0f;
    void Start() 
    {
        DeathTrigger.OnDeath += ResetOnDeath;
    }
	void FixedUpdate()
	{						
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
        
		anim.SetBool (Utility.ANIM_GROUND, grounded);
        
        if (grounded == true)
        {
            anim.SetFloat(Utility.ANIM_SPEED, Mathf.Abs(rigidbody2D.velocity.x));
            anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
        }
        else
        {
            anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
            anim.SetFloat(Utility.ANIM_VSPEED, rigidbody2D.velocity.y);
        }
		rigidbody2D.velocity = new Vector2 (movement * maxSpeed, rigidbody2D.velocity.y);
	}
	public void Move(float move)
	{
		movement = move;
		if (move > 0 && !facingRight) 
		{
			Flip ();
		} 
		else if (move < 0 && facingRight ) 
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

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    void ResetOnDeath() 
    {
        anim.SetFloat(Utility.ANIM_SPEED, Utility.ZERO);
        anim.SetFloat(Utility.ANIM_VSPEED, Utility.ZERO);
    }
}
