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

        // 10•b‚²‚Æ‚É“G‚Ì–hŒä—Í‚ð10ƒAƒbƒv
        InvokeRepeating(nameof(IncreaseEnemyShield), 10f, 10f);
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
        enemyShield += 10;
        UpdateShieldUI();
    }

    void UpdateShieldUI()
    {
        playerShieldText.text = "–hŒä—Í : " + playerShield;
        enemyShieldText.text = "–hŒä—Í : " + enemyShield;
    }
}