using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class LevelMapController : MonoBehaviour {
	
	private int level = 0;
	private Button [] buttons = null;

	private void Awake()
	{
		SetButtons ();
 	}

	public void SetLevel(int newLevel)
	{
		if (this.level >= newLevel)
		{
			return;		
		}
		PlayerPrefs.SetInt ("Level", newLevel);
		SetButtons ();
	}

	private void SetButtons()
	{
		if (this.buttons == null || this.buttons.Length == 0) 
		{
			this.buttons = this.gameObject.GetComponentsInChildren<Button> ();
			Array.Sort (this.buttons, (x,y) => String.Compare (x.name, y.name));
		}
		this.level = PlayerPrefs.GetInt("Level", 0);
		foreach (Button btn in this.buttons) 
		{
			btn.interactable = false;
		}

		for (int i = 0; i <= this.level; i++) 
		{
			this.buttons[i].interactable = true;
			this.buttons[i].GetComponent<Image>().color = Color.green;
			string level = "Level" + i.ToString();
			this.buttons[i].onClick.AddListener(()=>{ Debug.Log(level);});
		}
	}
}
