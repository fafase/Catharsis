using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    [SerializeField] protected CollectibleItem item = CollectibleItem.SmallCoin;
    public CollectibleItem Item { get { return item; } }
}

public enum CollectibleItem 
{ 
    SmallCoin, LargeCoin
}
