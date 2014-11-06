using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

    [SerializeField]
    private GUITexture lifeTexture = null, soulTexture = null;
    [SerializeField]
    private GUIText lifeText = null, soulText = null;
	void Start () 
    {
        PositionInGameGUI();
	}
    float width, height;
	void Update () 
    {
        if (width != Screen.width || height != Screen.height)
        {
            PositionInGameGUI();
        }
	}

    void PositionInGameGUI()
    {
        height = Screen.height;
        width = Screen.width;
        Vector2 lifeTextOffset = lifeText.pixelOffset;
        Vector2 soulTextOffset = soulText.pixelOffset;
        Rect lifeTextureRect = lifeTexture.pixelInset;
        Rect soulTextureRect = soulTexture.pixelInset;
        float margin = Screen.width / 60f;
        float size = 30;
        
        soulTextureRect = new Rect(margin,height  - size, size, size);
        soulTexture.pixelInset = soulTextureRect;
        soulTextOffset = new Vector2(margin + size, height - size);
        soulText.pixelOffset = soulTextOffset;

        lifeTextOffset = new Vector2(width - margin, height - size);
        lifeText.pixelOffset = lifeTextOffset;
        float textureMargin = lifeText.GetScreenRect().width;
        lifeTextureRect = new Rect(lifeTextOffset.x - textureMargin - size, height - size, size, size);
        lifeTexture.pixelInset = lifeTextureRect;  
    }
}
