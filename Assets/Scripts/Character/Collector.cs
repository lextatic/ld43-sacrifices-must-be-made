using UnityEngine;

public class Collector : MonoBehaviour
{
	public SpriteRenderer SacrificeSprite;
	public Inventory MyInventory;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Sacrifice") && MyInventory.CarriedItem == Inventory.Item.NONE)
		{
			SacrificeSprite.sprite = other.GetComponentInChildren<SpriteRenderer>().sprite;
			SacrificeSprite.enabled = true;

			MyInventory.CarriedItem = other.GetComponentInChildren<EnemyType>().Value;

			Destroy(other.gameObject);
		}
	}
}
