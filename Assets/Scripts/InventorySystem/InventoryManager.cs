using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private bool isInventoryOpen = false;
	public GameObject inventoryUI;

	private void Update()
	{
		if (InputControl.Current.Gameplay.Inventory.WasPerformedThisFrame())
		{
			ToggleInventory();
		}
	}

	public void ToggleInventory()
	{
		isInventoryOpen = !isInventoryOpen;
		inventoryUI.SetActive(isInventoryOpen);

		Time.timeScale = isInventoryOpen ? 0 : 1;
	}

	public bool AddItem(Item item)
    {
		for (int i = 0; i < inventorySlots.Length; i++)
		{
			InventorySlot slot = inventorySlots[i];
			InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
			if (itemSlot != null && itemSlot.item == item && itemSlot.count < item.maxItemCount && itemSlot.item.isStackable)
			{
				itemSlot.count++;
                itemSlot.RefreshCount();
				return true;
			}
		}

		for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }          
        }
		return false;
	}

    private void SpawnNewItem(Item item,InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem invItem= newItem.GetComponent<InventoryItem>();   
        invItem.InitializeItem(item);
    }
}
