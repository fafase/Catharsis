using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour 
{
	[SerializeField] private int price = 1;
	[SerializeField] private int level = 0;
	public int Price { get { return this.price; } }
	public int Level { get { return this.level; } }

	private void Awake()
	{
		string name = this.name;
		string [] values = name.Split ('(', ')');
		this.level = 0;
		if (values.Length > 1) 
		{
			level = int.Parse (values [1]);
		}
		this.level++;
	}
}
