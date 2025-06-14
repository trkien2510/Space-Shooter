using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject pauseMenu;

    public void LoadHome()
    {
        AudioManager.Instance.SetBGMVolume(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        AudioManager.Instance.SetBGMVolume(0f);
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
