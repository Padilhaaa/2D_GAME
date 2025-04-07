using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public ItemType type;

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
