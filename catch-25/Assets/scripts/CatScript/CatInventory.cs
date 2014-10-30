using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CatInventory : MonoBehaviour {

    [SerializeField] private Text text; 
    private int coinAmount;
    public event Action<int> OnAddCoin = (int i) => { };
    void Start() 
    {
        coinAmount = 0;
        text.text = coinAmount.ToString();
    }

    public void CoinAmount(int value) 
    {
        coinAmount += value;
        text.text = coinAmount.ToString();
        OnAddCoin(value);
    }
}
