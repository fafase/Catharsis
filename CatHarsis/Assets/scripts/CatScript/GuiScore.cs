using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiScore : MonoBehaviour {

	[SerializeField] private CatHealth catHealth;
    [SerializeField] private CatInventory catInventory;
	[SerializeField] private Text healthText;
    [SerializeField] private Text soulText;

	void Start () 
	{
		catHealth.OnChangeLives += UpdateHealthGUIText;
        catInventory.OnAddSoul += UpdateSoulGUIText;
		UpdateHealthGUIText ();
	}

	void UpdateHealthGUIText () 
	{
		healthText.text = "Deaths: " +catHealth.Lives.ToString()+"/9";
	}
    void UpdateSoulGUIText(int value)
    {
        soulText.text = value.ToString();
    }
}
