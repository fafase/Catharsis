using UnityEngine;
using System.Collections;

public abstract class GUIController : MonoBehaviour {

    [SerializeField]
    IGUIButton guiButton;

	protected virtual void Awake () 
    {
        InputMobileController.Instance.OnTap += guiButton.CheckForHitButton;
	}
}

public abstract class IGUIButton : MonoBehaviour
{
    public abstract void CheckForHitButton(Vector3 position);
}

