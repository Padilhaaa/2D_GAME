using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTrash : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();

		if (item != null)
		{
			Destroy(item.gameObject);
		}
	}
}
