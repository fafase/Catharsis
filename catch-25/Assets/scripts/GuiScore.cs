using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiScore : MonoBehaviour {

	[SerializeField] private CatHealth catHealth;
	[SerializeField] private Text text;

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
