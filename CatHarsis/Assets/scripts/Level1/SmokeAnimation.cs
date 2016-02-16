using UnityEngine;
using System.Collections;

public class SmokeAnimation : MonoBehaviour {

	public void ResetObject(){ this.gameObject.GetComponent<SpriteRenderer>().enabled = false;}
}
