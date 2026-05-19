using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwordManager : MonoBehaviour
{
    public int playerSword = 100;
    public int enemySword = 100;

    public Text playerSwordText;
    public Text enemySwordText;

    void Start()
    {
        UpdateSwordUI();
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

    void UpdateSwordUI()
    {
        playerSwordText.text = "çUåÇóÕ : " + playerSword;
        enemySwordText.text = "çUåÇóÕ : " + enemySword;
    }
}
