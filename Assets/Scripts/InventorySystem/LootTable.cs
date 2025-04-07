using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot System/Loot Table")]
public class LootTable : ScriptableObject
{
	[System.Serializable]
	public class LootEntry
	{
		public LootItem item;
		public GameObject itemPrefab;
		public int amount;
	}

	public LootEntry[] lootEntries;

	public LootEntry[] GetLoot()
	{
		return lootEntries;
	}
}
