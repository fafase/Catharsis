using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {

    [SerializeField]
    private GUITexture backTexture;
    [SerializeField]
    private GUIText introText;

    [SerializeField]
    private float speed;

	void Awake () 
    {
        FindObjectOfType<InputManager>().OnPress += LoadNextOnPress;
      
        StartCoroutine(AppearText());
	}

    private void LoadNextOnPress() 
    {
        StartCoroutine(RemoveText());
    }
    private IEnumerator AppearText() 
    {

        Color col = Color.white;
        col.a = 0f;
        backTexture.color = col;
        introText.color = col;

        while (backTexture.color.a < 1f)
        {
            //col = introText.color;
            col.a += Time.deltaTime * 1/ speed;
            backTexture.color = col;
            yield return null;
        }
        col.a = 0f;
        while (introText.color.a < 1f)
        { 
            //colText = introText.color;
            col.a += Time.deltaTime * 1 / speed;
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
