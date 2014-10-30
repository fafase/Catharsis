using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.2f;
    [SerializeField] private Color colorTarget;
    [SerializeField] private Image fadeImage;
    private FadeState fadeState = FadeState.None;

    private bool fadeOver = false;
    public bool FadeOver { get { return fadeOver; } }
    void Start() 
    {
        fadeImage.gameObject.SetActive(true);
    }

    void Update() 
    {
        switch (fadeState)
        {
            case FadeState.None:
                return;
            case FadeState.FadeIn:
                FadeIn();
                break;
            case FadeState.FadeOut:
                FadeOut();
                break;
        }
    }
    public void SetStart() 
    {      
        Color col = fadeImage.color;
        col.a = 1;
        fadeImage.color = col;
    }
    public enum FadeState
    {
        None, FadeIn, FadeOut
    }

    public void ChangeFadeState(FadeState newFadeState) 
    {
        fadeState = newFadeState;
        fadeOver = false;
    }
    
    private void FadeIn()
    {
        Color col = fadeImage.color;
        float value = Time.deltaTime * 1 / fadeTime;
        col.a -= value;
        fadeImage.color = col;
        if (col.a <= 0)
        {
            fadeState = FadeState.None;
            fadeOver = true;
        }
    }
    private void FadeOut() 
    {
        Color col = fadeImage.color;
        float value = Time.deltaTime * 1 / fadeTime;
        col.a += value;
        fadeImage.color = col;
        if (col.a >= 1)
        {
            fadeState = FadeState.None;
            fadeOver = true;
        }
    }
}


