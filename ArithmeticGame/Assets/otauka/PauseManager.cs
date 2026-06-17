using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    
    public string NextSceneText;
    // ˆê’â~
    public void PauseGame()
    {
        Time.timeScale = 0f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
    }

    // ÄŠJ
    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
    public void EndGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(NextSceneText);

    }
}
