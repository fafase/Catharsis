using UnityEngine;
using System.Collections;

public class UIControllerLevel : UIController 
{
	protected override void FadeInDone()
	{
		OnFadeInDone (null);
	}
}
