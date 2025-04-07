using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager instance;

	public PlayerData playerData;

	private string filePath;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(instance);
		}
		else Destroy(gameObject);
		filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
		playerData = new();
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

	public void Delete()
	{
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
	}

}
