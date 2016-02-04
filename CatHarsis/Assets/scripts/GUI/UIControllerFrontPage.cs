using UnityEngine;
using System.Collections;

public class UIControllerFrontPage : UIController 
{
	private void Awake()
	{
		this.fadeController.StartFade ("FadeIn", null);
	}
}
