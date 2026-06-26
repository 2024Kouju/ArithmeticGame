using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject panel0;

    void Start()
    {
        panel0.SetActive(false); // Å‰‚Í•Â‚¶‚é
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel0.activeSelf)
            {
                ClosePanel0();
            }
            else
            {
                OpenPanel0();
            }
        }
    }

    public void OpenPanel0()
    {
        panel0.SetActive(true);
        Time.timeScale = 0f;   // ƒQ[ƒ€’â~
    }

    public void ClosePanel0()
    {
        panel0.SetActive(false);
        Time.timeScale = 1f;   // ƒQ[ƒ€ÄŠJ
    }
}