using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleController : MonoBehaviour 
{
	public float amplitude = 5f;
	public float offset = 10f;
	public float omega = 1f;
	private Shadow [] shadows = null;
	private RectTransform[] rects = null;
	private Text[] texts = null;
	[SerializeField] private Animator animator = null;
	private int length = 0;

	private void Start () 
	{
		this.shadows = this.gameObject.GetComponentsInChildren<Shadow> ();
		this.length = this.shadows.Length;
		this.texts = new Text[this.length];
		this.rects = new RectTransform[this.length];
		for (int i = 0; i < this.length; i++) 
		{
			this.texts[i] = this.shadows[i].GetComponent<Text>();
			this.rects[i] = this.shadows[i].GetComponent<RectTransform>();
		}
		this.animator.Play ("Title", 0, 0f);
	}

	private void Update () 
	{
		for (int i = 0; i < this.length; i++) 
		{
			float value = Mathf.Cos (omega * (Time.time + i) ) * amplitude;
			this.shadows[i].effectDistance = new Vector2(0, value - offset);
			Vector2 anchoredPos = this.rects[i].anchoredPosition;
			anchoredPos.y = value;
			this.rects[i].anchoredPosition = anchoredPos;
		}
	}
}
