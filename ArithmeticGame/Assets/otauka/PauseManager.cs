using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

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
}
