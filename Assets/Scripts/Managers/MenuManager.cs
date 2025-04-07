using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public string sceneNameToLoad;
	public GameObject Loading;

	public void Start()
	{
		SaveManager.instance.Load();
	}
	public void PlayGame()
	{
		StartCoroutine(LoadingCoroutine());
	}

	public IEnumerator LoadingCoroutine()
	{
		Loading.SetActive(true);
		yield return new WaitForSeconds(2f); //Loading delay to see loading screen
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
