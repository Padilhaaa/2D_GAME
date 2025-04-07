using UnityEngine;
using TMPro;

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

	public void UseItem(InventoryItem itemSlot)
	{
		if (itemSlot == null || itemSlot.item == null) return;


		if (itemSlot.item.type == ItemType.Consumable)
		{
			if (!player.HandleConsumable(itemSlot.item)) return;

			itemSlot.count--;
			itemSlot.RefreshCount();
			if (itemSlot.count <= 0)
			{
				Destroy(itemSlot.gameObject);
			}
		}
	}


}
