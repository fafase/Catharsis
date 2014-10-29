using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    [SerializeField]
    private Text text;
    [SerializeField]
    private Image bg;
    [SerializeField]
    private Image cat;

    private float timer = 1f;
    private int index = 0;
    string[] str = {"Dreaming.", "Dreaming..", "Dreaming..?"};
	void Awake () 
    {
        text.text = str[index];
        GameHandler.OnChangeState += RemoveSreen;
	}
    private bool isLoading = true;
	void Update () 
    {
        timer -= Time.deltaTime;
        if (isLoading)
        {
            if (timer <= 0.0f)
            {
                timer = 1f;
                if (++index == str.Length)
                {
                    index = 0;
                }
                text.text = str[index];
            }
        }
        else 
        {
            Color col = bg.color;
            col.a = Mathf.Lerp(col.a, 0.0f, 5f * Time.deltaTime);
            bg.color = col;
            cat.color = col;
            text.color = col;
            if (col.a <= 0.0f)
            {
                bg.gameObject.SetActive(false);
                cat.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }
        }
	}

    private void RemoveSreen(string newState) 
    {
        if (newState == Utility.GAME_STATE_PLAYING)
        {
            isLoading = false;
        }
    }
}
