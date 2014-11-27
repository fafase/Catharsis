using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class IntroManager : MonoBehaviour 
{

    [SerializeField]
    private Image backTexture;
    [SerializeField]
    private Text introText;

    [SerializeField]
    private float speed;

	private bool callOnce = false;
	void Awake () 
    {          
        StartCoroutine(AppearText());
	}

    private void LoadNextOnPress(Vector3 v) 
    {
		if (callOnce == false) 
		{
			StartCoroutine (RemoveText ());			
		}
    }
    private IEnumerator AppearText() 
    {

        Color col = Color.white;
        col.a = 0f;
        backTexture.color = col;
        introText.color = col;

        while (backTexture.color.a < 1f)
        {
            col.a += Time.deltaTime * 1/ speed;
            backTexture.color = col;
            yield return null;
        }
        col.a = 0f;
        while (introText.color.a < 1f)
        { 
            col.a += Time.deltaTime * 1 / speed;
            introText.color = col;
            yield return null;
        }
		InputManager.Instance.OnTouch += LoadNextOnPress;
    }
 
    private IEnumerator RemoveText() 
    {
		callOnce = true;
		Color col = introText.color;
        while (introText.color.a > 0)
        {
			print (introText.color.a);
			col.a -= Time.deltaTime * 1 / speed;
            introText.color = col;
            yield return null;
        }
        Application.LoadLevel(Utility.LEVEL_1);
    }
}
