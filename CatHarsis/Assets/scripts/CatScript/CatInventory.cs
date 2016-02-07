using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SoulCollectionEventArg:System.EventArgs
{
	public readonly int soulCollected = 0;
	public SoulCollectionEventArg(int soul)
	{
		this.soulCollected = soul;
	}
}

public class CatInventory 
{
	public int SoulAmount { get; private set;}

	public CatInventory() { }

    public int SetSoulsAmount(int value) 
    {
        this.SoulAmount += value;
		return this.SoulAmount;
    }
}
