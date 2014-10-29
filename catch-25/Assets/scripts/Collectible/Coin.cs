using UnityEngine;
using System.Collections;


public class Coin : Collectible {

    [SerializeField] private int value;
    [SerializeField] private AudioClip sounds;
    private float startPosition;
    private float randomStart;
    [SerializeField]private float amplitude;
    [SerializeField] private float frequency;
    private Vector3 translatePosition;
    private Vector3 target;
    CatInventory ci = null;

    void Start() 
    {
        float distance = Mathf.Abs(Camera.main.transform.position.z);
        var frustumHeight = distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = -frustumHeight * Camera.main.aspect;

        translatePosition = new Vector3(frustumWidth, frustumHeight, 0f);
        randomStart = Random.Range(0.0f, Mathf.PI);
        startPosition = transform.position.y;
        GameObject cat = GameObject.FindGameObjectWithTag("Player");
        ci = cat.GetComponent<CatInventory>();
    }
    private bool isCollided = false;
    void Update() 
    {
        if (isCollided == false)
        {
            Vector3 position = transform.position;
            position.y = startPosition + amplitude * Mathf.Sin(frequency * Time.time + randomStart);
            transform.position = position;
        }
        else 
        {
            ResetTargetPoint();

            transform.position = Vector3.MoveTowards(transform.position,target, 10f *Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.1f) 
            {
               ci.CoinAmount += value;
               Destroy(gameObject);
            }
        }
    }

	void OnTriggerEnter2D (Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isCollided = true;
        }
	}

    void ResetTargetPoint() 
    {
        target = Camera.main.transform.position;
        target.z = 0f;
        target += translatePosition;
    }
}
