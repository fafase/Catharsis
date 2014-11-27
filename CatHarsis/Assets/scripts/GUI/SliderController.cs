using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderController : MonoBehaviour, IEndDragHandler 
{
    [SerializeField]
    private Slider slider;

    public void OnEndDrag(PointerEventData eventData)
    {
        slider.value = 0f;
    }
}
