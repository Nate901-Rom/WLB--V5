using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);//opens pause menu
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);//close pause menu
        Time.timeScale = 1;
    }

}
