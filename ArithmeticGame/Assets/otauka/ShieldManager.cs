using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShieldManager : MonoBehaviour
{
    public int playerShield = 100;
    public int enemyShield = 100;

    public Text playerShieldText;
    public Text enemyShieldText;

    void Start()
    {
        UpdateShieldUI();
    }

    public void AddPlayerShield(int value)
    {
        playerShield += value;
        UpdateShieldUI();
    }

    public void AddEnemyShield(int value)
    {
        enemyShield += value;
        UpdateShieldUI();
    }

    void UpdateShieldUI()
    {
        playerShieldText.text = "–hŒä—Í : " + playerShield;
        enemyShieldText.text = "–hŒä—Í : " + enemyShield;
    }
}
