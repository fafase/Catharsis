using UnityEngine;
using System.Collections;

public class UIAction : MonoBehaviour {
	[SerializeField] private UIController uiCtrl = null;

	public void LoadLevelFront(string level)
	{

		this.uiCtrl.SetLevel ("FadeOut", level);
	}
}
