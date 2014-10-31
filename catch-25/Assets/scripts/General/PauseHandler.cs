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
		Application.ExternalEval("window.open('"+faceBookUrl+"','_blank')");
	}

    public void ButtonWebSite() 
    {
		print ("Yep");
		Application.ExternalEval("window.open('"+webUrl+"','_blank')");
    }
	public void ButtonQASurvey() 
	{
		Application.ExternalEval("window.open('"+qaSurveyUrl+"','_blank')");
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
