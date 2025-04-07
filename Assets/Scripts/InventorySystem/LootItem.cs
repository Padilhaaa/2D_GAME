using UnityEngine;

public class LootItem : MonoBehaviour
{
	public Item itemData;

	private InventoryManager inventoryManager;

	public void Awake()
	{
		inventoryManager = FindFirstObjectByType<InventoryManager>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Colis�o detectada com: " + other.name);

		if (other.CompareTag("Player"))
		{		
			if (inventoryManager != null)
			{
				bool added = inventoryManager.AddItem(itemData);
				if (added)
				{
					Destroy(gameObject);
				}
			}
		}
	}
}
