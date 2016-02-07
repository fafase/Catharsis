using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiScore : MonoBehaviour 
{
	[SerializeField] private CatHealth catHealth = null;
    [SerializeField] private CatInventory catInventory = null;
	[SerializeField] private Text healthText = null;
    [SerializeField] private Text soulText = null;

	private void Start () 
	{
		this.catHealth.OnChangeLives += UpdateHealthGUIText;
		UpdateHealthGUIText ();
	}

	private void UpdateHealthGUIText () 
	{
		this.healthText.text = "Deaths: " + this.catHealth.Lives.ToString()+"/9";
	}
    public void UpdateSoulGUIText(int value)
    {
        this.soulText.text = value.ToString();
    }
}
