using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image image;
	public TMP_Text countText;

	[HideInInspector] public Transform parentAfterDrag;
	[HideInInspector] public Item item;

	public int itemCount = 1;
	private bool isHovered;

	public GameObject tooltipPanel;
	public TMP_Text tooltipText;

	private InventoryManager inventoryManager;

	public void Start()
	{
		inventoryManager = FindFirstObjectByType<InventoryManager>();
		InitializeItem(item);
	}

	private void Update()
	{
		if (isHovered && InputControl.Current.Gameplay.Consumable.WasPerformedThisFrame())
		{		
			if (inventoryManager != null)
			{
				inventoryManager.UseItem(this);
			}
		}
	}

	public void InitializeItem(Item newItem)
	{
		item = newItem;
		image.sprite = newItem.image;
		RefreshCount();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHovered = true;
		if(item != null)
		{
			tooltipPanel = inventoryManager.tooltipPanel;
			tooltipText = inventoryManager.tooltipText;
			tooltipText.text = $"{item.itemName}\n{item.description}";
			tooltipPanel.SetActive(true);
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHovered = false;
		tooltipPanel.SetActive(false);
	}


	public void RefreshCount()
	{
		countText.text = itemCount.ToString();
		countText.gameObject.SetActive(itemCount > 1);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		image.raycastTarget = false;
		parentAfterDrag = transform.parent;
		transform.SetParent(transform.root);
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = InputControl.Current.Gameplay.CursorPosition.ReadValue<Vector2>();//Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		image.raycastTarget = true;
		transform.SetParent(parentAfterDrag);
	}
}
