using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    private GUITexture guiBg;
    private float width, height;
	void Start () 
    {
        guiBg = GetComponent<GUITexture>();
        transform.position = Vector3.zero;
        transform.localScale = Vector3.zero;
        RescaleBackground();

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (width != Screen.width || height != Screen.height)
        {
            RescaleBackground();
        }
	}
    private void RescaleBackground() 
    {
        width = Screen.width;
        height = Screen.height;   
        Rect rect = new Rect(0f, 0f, width, height);
        guiBg.pixelInset = rect;
    }
}
