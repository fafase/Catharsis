using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeMessage : MonoBehaviour {

    [SerializeField]
    private string message;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Transform position;
    [SerializeField]
    private float messageLength = 3f;

    void Start() 
    {
        text.text = message;
    }
    private bool showMessage = false;

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player") && showMessage == false)
        {
            StartCoroutine(CountdownMessage());
        }
    }

    private IEnumerator CountdownMessage()
    {
        canvas.GetComponent<RectTransform>().position = position.position;
        canvas.gameObject.SetActive(true);
        showMessage = true;
        float timer = messageLength;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        canvas.gameObject.SetActive(false);
        showMessage = false;
    }
}
