using UnityEngine;
using System.Collections;


public class Coin : Collectible {

    [SerializeField] private int value;
    [SerializeField] private AudioClip sounds;
    private float startPosition;
    private float randomStart;
    [SerializeField]private float amplitude;
   [SerializeField] private float frequency;

    void Start() 
    {
        randomStart = Random.Range(0.0f, Mathf.PI);
        startPosition = transform.position.y;
    }
    void Update() 
    {
        Vector3 position = transform.position;
        position.y = startPosition + amplitude * Mathf.Sin(frequency * Time.time + randomStart);
        transform.position = position;
    }
	void OnTriggerEnter2D (Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<CatInventory>().CoinAmount += value;
            Destroy(gameObject);
        }
	}
}
