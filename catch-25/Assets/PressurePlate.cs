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
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") || col.gameObject.CompareTag ("DeadCat")) 
		{
			sprite.sprite = sprites[1];
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player") || col.gameObject.CompareTag ("DeadCat")) 
		{
			sprite.sprite = sprites[0];
		}
	}
}
