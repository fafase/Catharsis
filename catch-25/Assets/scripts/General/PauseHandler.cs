using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour {

    [SerializeField]
    private string faceBookUrl;
    [SerializeField]
    private string webUrl;
	
	public void ButtonFacebook () 
    {
        Application.OpenURL(faceBookUrl);
	}

    public void ButtonWebSite() 
    {
        Application.OpenURL(webUrl);
    }
}
