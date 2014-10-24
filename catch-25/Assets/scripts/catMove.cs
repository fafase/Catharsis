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

	private GameObject mainCamera;

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
		mainCamera = GameObject.Find("mainCamera");
	}
	
	void FixedUpdate()
	{
		// Check if player just died
		if(mainCamera.GetComponent<gameHandler>().playerLock){

		}
		else{

			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			anim.SetBool ("Ground", grounded);

			anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);

			float move = Input.GetAxis("Horizontal");

			anim.SetFloat ("speed", Mathf.Abs (move));

			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
			
			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight ) {
				Flip();						
			}
		}
	}

	void Update()
	{
		if (grounded) {
		
		}

		// korjaa tämä. laita hyppy input Manageriin !
		if(grounded && Input.GetKeyDown(KeyCode.UpArrow))
		{
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce(new Vector2(0, jumpForce));

		}
	}
	
	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
