using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager instance;

	public PlayerData playerData;

	private string filePath;

	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(instance);
		filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
	}

	void Start()
	{
		playerData = new();
		Load();
	}

	public void Save()
	{
		string jsonData = JsonUtility.ToJson(playerData, true);
		File.WriteAllText(filePath, jsonData);
	}

	public void Load()
	{
		if (File.Exists(filePath))
		{
			string jsonData = File.ReadAllText(filePath);
			playerData = JsonUtility.FromJson<PlayerData>(jsonData);
		}
	}

}
