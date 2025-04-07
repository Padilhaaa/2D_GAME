using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;
    public ConsumableType consumableType;

    public string itemName;
    public string description;

	public bool isStackable = true;
    public int maxItemCount;
}
[System.Serializable]
public class ItemQuantity
{
    public Item item;
    public int Count;
}

public enum ItemType
{
    Consumable,
    Tool,
    Armor,
    Collectables
}

public enum ConsumableType
{
    Health,
    Armor
}