using UnityEngine;

public class PauseManager : MonoBehaviour
{
	public GameObject pauseMenu;
	private bool isPaused = false;

	private void Update()
	{
		if (InputControl.Current.Gameplay.Pause.WasPerformedThisFrame())
		{
			TogglePause();
		}
	}

	public void TogglePause()
	{
		isPaused = !isPaused;
		pauseMenu.SetActive(isPaused);

		Time.timeScale = isPaused ? 0 : 1;
	}
}
