using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UITooltip : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
	[SerializeField] private GameObject tooltip = null;
	private void Awake()
	{
		HideTooltip ();
	}
	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		ShowTooltip ();
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		HideTooltip ();
	}

	#endregion

	#region IPointerDownHandler implementation

	public void OnPointerDown (PointerEventData eventData)
	{
		HideTooltip ();
	}

	#endregion

	private void ShowTooltip(){ this.tooltip.SetActive (true);}
	private void HideTooltip(){this.tooltip.SetActive (false);}
}
