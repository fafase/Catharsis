using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

	[SerializeField] private Sprite [] sprites;
	private SpriteRenderer sprite;
	// Use this for initialization
	void Start () 
	{
		sprite = GetComponent<SpriteRenderer> ();
		sprite.sprite = sprites [0];
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			sprite.sprite = sprites[1];
		}
	}
	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.CompareTag ("Player")) 
		{
			sprite.sprite = sprites[0];
		}
	}
}
