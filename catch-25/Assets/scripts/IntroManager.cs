using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {

    [SerializeField] private Text introText;
    [SerializeField]
    private FadeController fade;

	void Awake () 
    {
        FindObjectOfType<InputManager>().OnPress += LoadNextOnPress;
       
        fade.SetStart();
        fade.ChangeFadeState(FadeController.FadeState.FadeIn);
        StartCoroutine(AppearText());
	}

    private void LoadNextOnPress() 
    {
        StartCoroutine(RemoveText());
    }
    private IEnumerator AppearText() 
    {
        Color col = introText.color;
        col.a = 0f;
        introText.color = col;
        while (fade.FadeOver != true)
        {
            yield return null;
        }
        
        while (introText.color.a < 1)
        { 
            col = introText.color;
            col.a += Time.deltaTime;
            introText.color = col;
            yield return null;
        }
    }
 
    private IEnumerator RemoveText() 
    {
        while (introText.color.a > 0)
        {
            Color col = introText.color;
            col.a -= Time.deltaTime;
            introText.color = col;
            yield return null;
        }
        Application.LoadLevel(Utility.LEVEL_1);
    }
}
