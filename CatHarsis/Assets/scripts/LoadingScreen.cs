using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    [SerializeField]
    private Text text;
    [SerializeField]
    private Image bg;

    private float timer = 1f;
    private int index = 0;
    string[] str = {"Dreaming.", "Dreaming..", "Dreaming..?"};
	void Awake () 
    {
        text.text = str[index];
        FindObjectOfType<GameHandler>().RaiseChangeState += RemoveSreen;
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
            text.color = col;

            if (col.a <= 0.0f)
            {
                
                bg.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }
        }
	}

    private void RemoveSreen( object sender, StateEventArg arg) 
    {
        if (arg.currentState == Utility.GAME_STATE_PLAYING)
        {
            isLoading = false;
        }
    }
}
