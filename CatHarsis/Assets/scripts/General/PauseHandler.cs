using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour 
{

    [SerializeField]
    private string faceBookUrl;
    [SerializeField]
    private string webUrl;
	[SerializeField]
	private string qaSurveyUrl;

	public void ButtonFacebook () 
    {
        Application.OpenURL(faceBookUrl);
		Application.ExternalEval("window.open('"+faceBookUrl+"','_blank')");
	}

    public void ButtonWebSite() 
    {
        Application.OpenURL(webUrl);
		Application.ExternalEval("window.open('"+webUrl+"','_blank')");
    }
	public void ButtonQASurvey() 
	{
        Application.OpenURL(qaSurveyUrl);
		Application.ExternalEval("window.open('"+qaSurveyUrl+"','_blank')");
	}

    public void Restart() 
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Pause()
    {
        GameHandler gameHandler = FindObjectOfType<GameHandler>();
        gameHandler.OnPause();
    }
}
