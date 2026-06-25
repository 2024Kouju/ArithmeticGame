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
    public Text playerShieldStatusText;
    public Text enemyShieldStatusText;

    void Start()
    {
        UpdateShieldUI();

        playerShieldStatusText.gameObject.SetActive(false);
        enemyShieldStatusText.gameObject.SetActive(false);

        InvokeRepeating(nameof(IncreaseEnemyShield), 21f, 15f);
    }
    void ShowPlayerShieldStatus(int value)
    {
        playerShieldStatusText.gameObject.SetActive(true);

        if (value > 0)
        {
            playerShieldStatusText.text = "–hŒä—Í +" + value;
        }
        else
        {
            playerShieldStatusText.text = "–hŒä—Í " + value;
        }

        CancelInvoke(nameof(HidePlayerShieldStatus));
        Invoke(nameof(HidePlayerShieldStatus), 1f);
    }

    void HidePlayerShieldStatus()
    {
        playerShieldStatusText.gameObject.SetActive(false);
    }
    void ShowEnemyShieldStatus(int value)
    {
        enemyShieldStatusText.gameObject.SetActive(true);

        if (value > 0)
        {
            enemyShieldStatusText.text = "–hŒä—Í +" + value;
        }
        else
        {
            enemyShieldStatusText.text = "–hŒä—Í " + value;
        }

        CancelInvoke(nameof(HideEnemyShieldStatus));
        Invoke(nameof(HideEnemyShieldStatus), 1f);
    }

    void HideEnemyShieldStatus()
    {
        enemyShieldStatusText.gameObject.SetActive(false);
    }

    public void AddPlayerShield(int value)
    {
        playerShield += value;

        ShowPlayerShieldStatus(value);

        UpdateShieldUI();
    }

    public void AddEnemyShield(int value)
    {
        enemyShield += value;

        ShowEnemyShieldStatus(value);

        UpdateShieldUI();
    }

    void IncreaseEnemyShield()
    {
        enemyShield += 5;

        ShowEnemyShieldStatus(5);

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