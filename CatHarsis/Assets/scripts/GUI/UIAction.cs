using UnityEngine;
using System.Collections;

public class UIAction : MonoBehaviour {
	[SerializeField] private UIController uiCtrl = null;

	public void LoadLevel(string level)
	{
		this.uiCtrl.SetLevel ("FadeOut", level);
	}
}
