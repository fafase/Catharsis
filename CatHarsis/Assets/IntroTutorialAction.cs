using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroTutorialAction : MonoBehaviour 
{
	[SerializeField] private IntroTutorialController tutCtrl = null;

	private void Awake()
	{
		if (this.tutCtrl == null)
		{
			Debug.LogError ("[CatHarsis] Missing IntroTutorialController in IntroTutorialAction");	
		}
	}

	public void SkipTutorial () 
	{
		if (this.tutCtrl == null) 
		{
			Debug.LogError ("[CatHarsis] Lost IntroTutorialController in IntroTutorialAction");
		}
		this.tutCtrl.SetLevel (UIController.FadeParameter.FadeOut, "Tutorial");
	}

	public void SetNextStep()
	{
		if (this.tutCtrl == null) 
		{
			Debug.LogError ("[CatHarsis] Lost IntroTutorialController in IntroTutorialAction");
		}
		this.tutCtrl.SetNextStep ();
	}
}
