using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour {

    [SerializeField]
    private string faceBookUrl;
    [SerializeField]
    private string webUrl;
	[SerializeField]
	private string qaSurveyUrl;
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
	public void ButtonQASurvey() 
	{
		Application.OpenURL(qaSurveyUrl);
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
