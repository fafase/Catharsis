using UnityEngine;
using System.Collections;

public class UIAction : MonoBehaviour {
	[SerializeField] private UIControllerFrontPage uiCtrl = null;

	public void LoadLevelFront(string level)
	{
		this.uiCtrl.SetBoard ();
	}
	public void LoadLevel(string levelName)
	{
		this.uiCtrl.SetLevel ("FadeOut", levelName);
	}
}
