using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum Item
	{
		NONE,
		BIRD,
		MOUSE,
		GECKO
	}

	[HideInInspector]
	public Item CarriedItem;

	public void Start()
	{
		CarriedItem = Item.NONE;
	}
}
