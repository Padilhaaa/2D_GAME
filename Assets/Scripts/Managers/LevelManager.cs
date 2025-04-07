using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void MainMenu()
    {
        Time.timeScale = 1;
		SceneManager.LoadSceneAsync("Menu");
    }
}
