using UnityEngine;
using System.Collections;

/// <summary>
/// FrontPage scene
/// Manager object
/// Connect the InputMobileController with the GUIButton class
/// </summary>
public class IntroGUIController : GUIController {

    [SerializeField]
    private GUITexture playBtn = null;
    [SerializeField]
    private GUITexture settingBtn = null;

    [SerializeField]
    private float ratioWidthPlayBtn = 3f;
    [SerializeField]
    private float ratioHeightPlayBtn = 5f;

    [SerializeField]
    private float ratioSettingBtn = 10f;

    
	void Start () 
    {
        SetDimensionPositionPlayBtn(playBtn, playBtn.transform.localPosition, ratioWidthPlayBtn, ratioHeightPlayBtn);
        SetDimensionPositionSettingBtn(settingBtn, ratioSettingBtn);
	}

    private void SetDimensionPositionPlayBtn(GUITexture guiBtn,Vector3 position, float ratioWidth, float ratioHeight)
    {
        float width = Screen.width;
        float height = Screen.height;

        float btnWidth = width / ratioWidth;
        float btnHeight = height / ratioHeight;
        guiBtn.transform.localPosition = position;
        Rect rect = new Rect(-btnWidth / 2, -btnHeight / 2, btnWidth, btnHeight);
        guiBtn.pixelInset = rect;
    }
    private void SetDimensionPositionSettingBtn (GUITexture guiBtn, float ratio)
    {
        float width = Screen.width;
        float btnWidth = width / ratio;
        float btnHeight = btnWidth;
        guiBtn.transform.localPosition = new Vector3(1f, 0f, 2f);
        Rect rect = new Rect(-btnWidth, 0, btnWidth, btnHeight);
        guiBtn.pixelInset = rect;
    }
}
