using UnityEngine;
using System.Collections;

public class SettingGUIButton :MonoBehaviour 
{
	public void OnPressBackButton () 
    {
        Application.LoadLevel("FrontPage");
	}
}
