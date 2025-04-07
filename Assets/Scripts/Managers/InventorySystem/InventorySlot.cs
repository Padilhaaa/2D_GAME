using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	public void OnDrop(PointerEventData eventData)
	{
		InventoryItem draggedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
		InventoryItem targetItem = GetComponentInChildren<InventoryItem>();

		if (targetItem != null) 
		{
			Transform originalParent = draggedItem.parentAfterDrag;
			draggedItem.parentAfterDrag = targetItem.transform.parent;
			targetItem.transform.SetParent(originalParent);
		}
		else 
		{
			draggedItem.parentAfterDrag = transform;
		}
	}
}
