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

        // 10•b‚²‚Æ‚É“GHP‚ð10‰ñ•œ
        InvokeRepeating(nameof(HealEnemy), 10f, 10f);
    }

    public void AddPlayerHP(int value)
    {
        playerHP += value;

        UpdateHPUI();

        if (playerHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void AddEnemyHP(int value)
    {
        enemyHP += value;

        UpdateHPUI();

        if (enemyHP <= 0)
        {
            CancelInvoke(nameof(HealEnemy)); // “GŽ€–SŽž‚Í‰ñ•œ’âŽ~
            SceneManager.LoadScene("GameClear");
        }
    }

    void HealEnemy()
    {
        enemyHP += 10;
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        playerHPText.text = "HP : " + playerHP;
        enemyHPText.text = "HP : " + enemyHP;
    }
}