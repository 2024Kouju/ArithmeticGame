using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        // ƒvƒŒƒCƒ„[€–S
        if (playerHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void AddEnemyHP(int value)
    {
        enemyHP += value;

        UpdateHPUI();

        // “G€–S
        if (enemyHP <= 0)
        {
            SceneManager.LoadScene("GameClear");
        }
    }

    void UpdateHPUI()
    {
        playerHPText.text = "HP : " + playerHP;
        enemyHPText.text = "HP : " + enemyHP;
    }
}