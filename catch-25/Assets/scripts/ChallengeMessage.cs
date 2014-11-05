using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeMessage : MonoBehaviour {

    [SerializeField]
    private string message;
   
    private GameObject messageObject;
    [SerializeField]
    private TextMesh text;
    private Transform position;
    [SerializeField]
    private float messageLength = 3f;

    private bool showMessage = false;

	void Start()
	{
		foreach (Transform t in transform)
		{
			if(t == this.transform)
			{
				continue;
			}
			position = t;
		}
        messageObject = text.gameObject;
	}
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player") && showMessage == false)
        {
			messageObject.SetActive(true);
			messageObject.transform.position = position.position;
			showMessage = true;
			text.text = message;
            StartCoroutine(CountdownMessage());
        }
    }

    private IEnumerator CountdownMessage()
    {
        
        float timer = messageLength;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        messageObject.SetActive(false);
		Destroy (gameObject);
    }
}
