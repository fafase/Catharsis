using UnityEngine;
using System.Collections;

public class SettingGUIButton : IGUIButton 
{

    [SerializeField]
    private GUITexture backBtn;

    public override void CheckForHitButton(Vector3 position)
    {
        print("Here");
        if (backBtn.HitTest(position))
        {
            OnPressBackButton();
        }
    }

	private void OnPressBackButton () 
    {
        Application.LoadLevel("FrontPage");
	}
}
