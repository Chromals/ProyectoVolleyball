using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  
        pauseMenuUI.SetActive(true);  
    }

    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  
        pauseMenuUI.SetActive(false);  
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
