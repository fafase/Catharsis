using UnityEngine;
using System.Collections;

public class UIActionLevelMap : MonoBehaviour {

	[SerializeField] UiControllerLevelMap uiCtrl = null;
	public void ButtonActionLoadLevel()
	{
		this.uiCtrl.SetLoadLevel ();
	}
	public void ButtonActionReset(GameObject obj)
	{
		obj.SetActive (false);
	}
	public void ButtonActionSet(GameObject obj)
	{
		obj.SetActive (!obj.activeSelf);
	}
}
