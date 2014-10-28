using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CatInventory : MonoBehaviour {

    [SerializeField] private Text text; 
    private int coinAmount;

    void Start() 
    {
        coinAmount = 0;
        text.text = coinAmount.ToString();
    }
	public int CoinAmount
    {
        get { return coinAmount; }
        set { 
            
            coinAmount = value;
            text.text = coinAmount.ToString();
        }
    }

}
