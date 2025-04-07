using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Item")]
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