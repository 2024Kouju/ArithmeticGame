using UnityEngine;
using UnityEngine.UI;

public class ShieldManager : MonoBehaviour
{
    public static int FinalPlayerShield;
    public static int FinalEnemyShield;

    public int playerShield = 100;
    public int enemyShield = 100;

    public Text playerShieldText;
    public Text enemyShieldText;

    void Start()
    {
        UpdateShieldUI();

        InvokeRepeating(nameof(IncreaseEnemyShield), 15f, 15f);
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

    void IncreaseEnemyShield()
    {
        enemyShield += 5;
        UpdateShieldUI();
    }

    public void SaveFinalStatus()
    {
        FinalPlayerShield = playerShield;
        FinalEnemyShield = enemyShield;
    }

    void UpdateShieldUI()
    {
        if (playerShieldText != null)
            playerShieldText.text = "–hŒä—Í : " + playerShield;

        if (enemyShieldText != null)
            enemyShieldText.text = "–hŒä—Í : " + enemyShield;
    }
}