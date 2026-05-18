using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPManager : MonoBehaviour
{
    public int playerHP = 100;
    public int enemyHP = 100;

    public Text playerHPText;
    public Text enemyHPText;

    void Start()
    {
        UpdateHPUI();
    }

    public void AddPlayerHP(int value)
    {
        playerHP += value;
        UpdateHPUI();
    }

    public void AddEnemyHP(int value)
    {
        enemyHP += value;
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        playerHPText.text = "HP : " + playerHP;
        enemyHPText.text =  "HP : " + enemyHP;
    }
}
