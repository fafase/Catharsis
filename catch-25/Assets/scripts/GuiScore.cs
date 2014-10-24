using UnityEngine;
using System.Collections;

public class GuiScore : MonoBehaviour {

	[SerializeField] private CatHealth catHealth;
	[SerializeField] private GUIText text;

	void Start () 
	{
		catHealth.OnChangeLives += UpdateGUIText;
		UpdateGUIText ();
	}

	void UpdateGUIText () 
	{
		text.text = "Deaths: " +catHealth.Lives.ToString()+"/9";
	}
}
