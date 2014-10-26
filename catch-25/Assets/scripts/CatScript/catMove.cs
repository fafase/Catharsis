using UnityEngine;
using System.Collections;

public class catMove : MonoBehaviour {

	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float maxSpeed = 10f;
	bool facingRight = true;
	bool grounded = false;
	float groundRadius = 0.2f;
	public float jumpForce = 500f;

	//private GameObject mainCamera;

	[SerializeField] private Animator anim;
	private float movement = 0f;
	void Start()
	{
	//	mainCamera = GameObject.Find("mainCamera");
	}

	void FixedUpdate()
	{						
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		rigidbody2D.velocity = new Vector2 (movement * maxSpeed, rigidbody2D.velocity.y);
	}

	public void Move(float move)
	{
		anim.SetFloat ("speed", Mathf.Abs (move));
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
		anim.SetBool ("Ground", false);
		rigidbody2D.AddForce(new Vector2(0, jumpForce));
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
