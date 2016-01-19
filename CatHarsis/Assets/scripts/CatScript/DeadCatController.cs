using UnityEngine;
using System.Collections;

public class DeadCatController : MonoBehaviour 
{

	public void InitDeadCat(CatDeath catDeath, float scale)
	{
		print (catDeath);
		SetPosition (scale);
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingOrder = 2;
		switch (catDeath) 
		{
			case CatDeath.Crush:
				GetComponent<SpriteRenderer> ().sortingOrder = 0;
				gameObject.GetComponent<Collider2D>().enabled = false;  
				gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				break;
			case CatDeath.Gas:
				spriteRenderer.color = new Color(0.1f,0.6f,0.1f,1.0f);
				break;
			case CatDeath.Spike:
				gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
				break;
			case CatDeath.Flower:
				break;
		}
	}


	private void SetPosition(float scale)
	{
		if (scale < 0)
		{
			Vector3 newScale = transform.localScale;
			newScale.x *= -1;
			transform.localScale = newScale;
		}
	}
}

public enum CatDeath
{
	None, Spike, Gas, Fall, Crush, Flower
}
