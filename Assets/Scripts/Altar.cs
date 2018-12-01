using System;
using UnityEngine;
using static Inventory;

public class Altar : MonoBehaviour
{
	private int victoryCondition;
	private int sacrificesCount;

	public Inventory playerInventory;
	public SpriteRenderer playerSacrificeSprite;

	[Serializable]
	public struct TypeAndPrefab
	{
		public Item EnemyType;
		public GameObject Prefab;
	}

	public TypeAndPrefab[] TypeAndPrefabs;

	public void Start()
	{
		var enemies = FindObjectsOfType<EnemyType>();
		victoryCondition = enemies.Length;
		sacrificesCount = 0;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (playerInventory.CarriedItem != Inventory.Item.NONE)
			{
				playerSacrificeSprite.enabled = false;

				for(int i = 0; i < TypeAndPrefabs.Length; i++)
				{
					if(playerInventory.CarriedItem == TypeAndPrefabs[i].EnemyType)
					{
						Instantiate(TypeAndPrefabs[i].Prefab, transform.position + Vector3.up * 0.5f + Vector3.right * UnityEngine.Random.Range(-0.1f, 0.1f), Quaternion.identity);
					}
				}

				playerInventory.CarriedItem = Inventory.Item.NONE;

				sacrificesCount++;

				if(sacrificesCount == victoryCondition)
				{
					Debug.Log("Victory!");
				}
			}
		}
	}
}
