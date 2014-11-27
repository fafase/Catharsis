using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CatInventory : MonoBehaviour {

    private int soulAmount;
    public event Action<int> OnAddSoul = (int i) => { };
    void Start() 
    {
        soulAmount = 0;
    }

    public void SetSoulsAmount(int value) 
    {
        soulAmount += value;
        OnAddSoul(soulAmount);
    }
}
