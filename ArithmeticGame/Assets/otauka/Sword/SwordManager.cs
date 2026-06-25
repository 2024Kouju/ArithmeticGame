using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwordManager : MonoBehaviour
{
    public static int FinalPlayerSword;
    public static int FinalEnemySword;

    public int playerSword = 100;
    public int enemySword = 100;

    public Text playerSwordText;
    public Text enemySwordText;
    public Text playerSwordStatusText;
    public Text enemySwordStatusText;
    void Start()
    {
        UpdateSwordUI();

        playerSwordStatusText.gameObject.SetActive(false);
        enemySwordStatusText.gameObject.SetActive(false);

        InvokeRepeating(nameof(IncreaseEnemySword), 21f, 15f);
    }
    void ShowPlayerSwordStatus(int value)
    {
        playerSwordStatusText.gameObject.SetActive(true);

        if (value > 0)
        {
            playerSwordStatusText.text = "çUåÇóÕ +" + value;
        }
        else
        {
            playerSwordStatusText.text = "çUåÇóÕ " + value;
        }

        CancelInvoke(nameof(HidePlayerSwordStatus));
        Invoke(nameof(HidePlayerSwordStatus), 1f);
    }

    void HidePlayerSwordStatus()
    {
        playerSwordStatusText.gameObject.SetActive(false);
    }

    void ShowEnemySwordStatus(int value)
    {
        enemySwordStatusText.gameObject.SetActive(true);

        if (value > 0)
        {
            enemySwordStatusText.text = "çUåÇóÕ +" + value;
        }
        else
        {
            enemySwordStatusText.text = "çUåÇóÕ " + value;
        }

        CancelInvoke(nameof(HideEnemySwordStatus));
        Invoke(nameof(HideEnemySwordStatus), 1f);
    }

    void HideEnemySwordStatus()
    {
        enemySwordStatusText.gameObject.SetActive(false);
    }
    public void AddPlayerSword(int value)
    {
        playerSword += value;
        UpdateSwordUI();
    }

    public void AddEnemySword(int value)
    {
        enemySword += value;
        ShowPlayerSwordStatus(value);
        UpdateSwordUI();
    }

    void IncreaseEnemySword()
    {
        enemySword += 5;

        ShowEnemySwordStatus(5);

        UpdateSwordUI();
    }
    public void SaveFinalStatus()
    {
        FinalPlayerSword = playerSword;
        FinalEnemySword = enemySword;
    }
    void UpdateSwordUI()
    {
        playerSwordText.text = "çUåÇóÕ : " + playerSword;
        enemySwordText.text = "çUåÇóÕ : " + enemySword;
    }
}