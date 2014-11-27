using UnityEngine;
using System.Collections;

public class IntroGUIButton : IGUIButton 
{
    [SerializeField]
    private GUITexture playBtn = null;
    [SerializeField]
    private GUITexture settingBtn = null;

    public override void CheckForHitButton(Vector3 position)
    {
        if (playBtn.HitTest(position))
        {
            OnPressPlay();
        }
        else if (settingBtn.HitTest(position))
        {
            OnPressSetting();
        }
    }

    private void OnPressPlay()
    {
        Application.LoadLevel("LevelSelector");
    }

    private void OnPressSetting()
    {
        Application.LoadLevel("Settings");
    }
}
