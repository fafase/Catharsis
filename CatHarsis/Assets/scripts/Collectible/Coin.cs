using UnityEngine;
using System.Collections;


public class Coin : Collectible 
{
    [SerializeField] private int value;
    [SerializeField] private AudioClip sounds;
    private float startPosition;
    private float randomStart;
    [SerializeField]private float amplitude;
    [SerializeField] private float frequency;
    private Vector3 translatePosition;
    private Vector3 target;
    private CatController catController = null;
	private bool isCollided = false;

    private void Start() 
    {
        float distance = Mathf.Abs(Camera.main.transform.position.z);
        var frustumHeight = distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = -frustumHeight * Camera.main.aspect;

        translatePosition = new Vector3(frustumWidth, frustumHeight, 0f);
        randomStart = Random.Range(0.0f, Mathf.PI);
        startPosition = transform.position.y;
        GameObject cat = GameObject.FindGameObjectWithTag("Player");
		this.catController = cat.GetComponent<CatController> ();
    }

    private void Update() 
    {
        if (this.isCollided == false)
        {
            Vector3 position = this.transform.position;
            position.y = this.startPosition + this.amplitude * Mathf.Sin(this.frequency * Time.time + this.randomStart);
            this.transform.position = position;
        }
        else 
        {
            ResetTargetPoint();

            this.transform.position = Vector3.MoveTowards(this.transform.position,target, 10f *Time.deltaTime);
            if (Vector3.Distance(this.transform.position, this.target) < 0.1f) 
            {
               this.catController.SetSoulsAmount(value);
               Destroy(this.gameObject);
            }
        }
    }

	private void OnTriggerEnter2D (Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            this.isCollided = true;
        }
	}

    private void ResetTargetPoint() 
    {
        this.target = Camera.main.transform.position;
        this.target.z = 0f;
        this.target += translatePosition;
    }
}
