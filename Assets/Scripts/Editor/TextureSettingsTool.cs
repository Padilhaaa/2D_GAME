using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureSettingsTool
{
	[MenuItem("Tools/Apply Point Filter & No Compression")]
	static void ApplySettings()
	{
		string[] guids = AssetDatabase.FindAssets("t:Texture");
		foreach (string guid in guids)
		{
			string path = AssetDatabase.GUIDToAssetPath(guid);
			TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

			if (importer != null)
			{
				importer.textureCompression = TextureImporterCompression.Uncompressed;
				importer.filterMode = FilterMode.Point;
				importer.SaveAndReimport();
			}
		}

		Debug.Log("All textures were adjusted!");
	}
}