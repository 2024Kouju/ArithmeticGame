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

    void Start()
    {
        UpdateSwordUI();

        // 10ïbÇ≤Ç∆Ç…ìGçUåÇóÕ+10
        InvokeRepeating(nameof(IncreaseEnemySword), 15f, 15f);
    }

    public void AddPlayerSword(int value)
    {
        playerSword += value;
        UpdateSwordUI();
    }

    public void AddEnemySword(int value)
    {
        enemySword += value;
        UpdateSwordUI();
    }

    void IncreaseEnemySword()
    {
        enemySword += 5;
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