using UnityEngine;
using System.Collections;

public class IntroGUIButton : MonoBehaviour 
{ 
    public void OnPressPlay()
    {
        Application.LoadLevel("LevelSelector");
    }

    public void OnPressSetting()
    {
        Application.LoadLevel("Settings");
    }
}
