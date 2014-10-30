using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour {

    [SerializeField]
    private string faceBookUrl;
    [SerializeField]
    private string webUrl;
    [SerializeField]
    private InputManager inputManager;
	public void ButtonFacebook () 
    {
        Application.OpenURL(faceBookUrl);
	}

    public void ButtonWebSite() 
    {
        Application.OpenURL(webUrl);
    }
    void OnEnable() 
    {
        inputManager.OnRestart += this.Restart;
    }
    void OnDisable()
    {
        inputManager.OnRestart -= this.Restart;
    }
    private void Restart() 
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
