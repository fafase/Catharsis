using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour 
{
	[SerializeField] private GameObject frame = null;
	public void Init()
	{
		this.frame.SetActive(false);
	}
}
