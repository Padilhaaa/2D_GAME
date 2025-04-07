using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    private bool isInventoryOpen = false;
	public GameObject inventoryUI;

	public GameObject tooltipPanel;
	public TMP_Text tooltipText;

	private PlayerController player;

	public void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		LoadInventory();
	}

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
			if (itemSlot != null && itemSlot.item == item && itemSlot.itemCount < item.maxItemCount && itemSlot.item.isStackable)
			{
				itemSlot.itemCount++;
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

	public void AddItem(Item item, int slotToInsert, int count)
	{
		InventorySlot slot = inventorySlots[slotToInsert];
		SpawnNewItem(item, slot);
		InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>();
		itemSlot.itemCount = count;
		itemSlot.RefreshCount();
	}

	private void SpawnNewItem(Item item,InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem invItem= newItem.GetComponent<InventoryItem>();   
        invItem.InitializeItem(item);
    }

	public void UseItem(InventoryItem itemSlot)
	{
		if (itemSlot == null || itemSlot.item == null) return;


		if (itemSlot.item.type == ItemType.Consumable)
		{
			if (!player.HandleConsumable(itemSlot.item)) return;

			itemSlot.itemCount--;
			itemSlot.RefreshCount();
			if (itemSlot.itemCount <= 0)
			{
				Destroy(itemSlot.gameObject);
			}
		}
	}

	public void SaveInventory()
	{
		List<ItemQuantity> itensToSave = new List<ItemQuantity>();

		for (int i = 0; i < inventorySlots.Length; i++)
		{
			InventoryItem item = inventorySlots[i].GetComponentInChildren<InventoryItem>();
			if (item == null) itensToSave.Add(null);
			else
			{
				ItemQuantity itemQuantity = new();
				itemQuantity.item = item.item;
				itemQuantity.Count = item.itemCount;
				itensToSave.Add(itemQuantity); 
			}
		}

		SaveManager.instance.playerData.items = itensToSave;
		SaveManager.instance.Save();
	}

	public void LoadInventory()
	{
		List<ItemQuantity> itensToLoad = new List<ItemQuantity>();
		itensToLoad = SaveManager.instance.playerData.items;

		if (itensToLoad == null) return;

		for (int i = 0; i < inventorySlots.Length; i++)
		{
			ItemQuantity item = itensToLoad[i];
			if (item.item != null)
			{
				AddItem(itensToLoad[i].item, i, itensToLoad[i].Count);
			}
		}
	}

}
