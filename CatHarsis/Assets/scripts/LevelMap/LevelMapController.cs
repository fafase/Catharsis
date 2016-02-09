using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class LevelMapController : MonoBehaviour {

	[SerializeField] private UiControllerLevelMap uiCtrl = null;

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
			//Array.Sort (this.buttons, (x,y) => String.Compare (x.name, y.name));
		}
		this.level = 3;// PlayerPrefs.GetInt("Level", 0);
		foreach (Button btn in this.buttons) 
		{
			btn.interactable = false;
			Color col = btn.GetComponent<Image> ().color;
			col.a = 0.5f;
			btn.GetComponent<Image> ().color = col;
		}

		for (int i = 0; i <= this.level; i++) 
		{
			this.buttons[i].interactable = true;
			Color col = this.buttons [i].GetComponent<Image> ().color;
			col.a = 1f;
			this.buttons [i].GetComponent<Image> ().color = col;
			string level = "Level" + i.ToString();
			LevelData ld = this.buttons [i].GetComponent<LevelData> ();
			this.buttons[i].onClick.AddListener(()=>{ DisplayLevelEntry(ld);});
		}
	}

	public void DisplayLevelEntry(LevelData levelData)
	{
		this.uiCtrl.SetLevelPrice (levelData);
	}
}
