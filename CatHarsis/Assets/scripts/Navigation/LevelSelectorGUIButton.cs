using UnityEngine;
using System.Collections;

public class LevelSelectorGUIButton : MonoBehaviour {
	[SerializeField]
	private int offsetLevel = 0;

	public void OnPressLevelButton(int level)
	{
		int temp = level + offsetLevel;
		Application.LoadLevel (temp);
	}
}
