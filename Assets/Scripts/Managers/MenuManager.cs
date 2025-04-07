using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public string sceneNameToLoad;
	public GameObject Loading;
	public GameObject LoadButton;

	public void Start()
	{
		if(SaveManager.instance.playerData.Level < 2) LoadButton.SetActive(false);
	}
	public void PlayGame()
	{
		StartCoroutine(LoadingCoroutine());
	}

	public void LoadGame()
	{
		sceneNameToLoad = "Level_" + SaveManager.instance.playerData.Level.ToString();
		StartCoroutine(LoadingCoroutine());
	}

	public IEnumerator LoadingCoroutine()
	{
		Loading.SetActive(true);
		yield return new WaitForSeconds(2f);//Loading delay to see loading
		SceneManager.LoadSceneAsync(sceneNameToLoad);
	} 

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
