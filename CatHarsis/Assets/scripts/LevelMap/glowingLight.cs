using UnityEngine;
using System.Collections;

public class glowingLight : MonoBehaviour {
	
	public float fadeSpeed = 1f;            // How fast the light fades between intensities.
	public float highIntensity = 4f;        // The maximum intensity of the light whilst the alarm is on.
	public float lowIntensity = 1f;       // The minimum intensity of the light whilst the alarm is on.
	public float changeMargin = 0.1f;       // The margin within which the target intensity is changed.
	public bool glowOn;                    // Whether or not the alarm is on.
	private float targetIntensity;          // The intensity that the light is aiming for currently.

	// Use this for initialization
	void Start () {
	
	}

	void Awake () {
		GetComponent<Light>().intensity = lowIntensity;
		targetIntensity = highIntensity;
		glowOn = true;
	}

	// Update is called once per frame
	void Update () {

		if(glowOn)
		{
			// ... Lerp the light's intensity towards the current target.
			GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, targetIntensity, fadeSpeed * Time.deltaTime);
			// Check whether the target intensity needs changing and change it if so.
			CheckTargetIntensity();
		}
		else
			// Otherwise fade the light's intensity to zero.
			GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0f, fadeSpeed * Time.deltaTime);
	}

	
	void CheckTargetIntensity ()
	{
		// If the difference between the target and current intensities is less than the change margin...
		if(Mathf.Abs(targetIntensity - GetComponent<Light>().intensity) < changeMargin)
		{
			// ... if the target intensity is high...
			if(targetIntensity == highIntensity)
				// ... then set the target to low.
				targetIntensity = lowIntensity;
			else
				// Otherwise set the targer to high.
				targetIntensity = highIntensity;
		}
	}
}