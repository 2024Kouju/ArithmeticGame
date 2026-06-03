using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    public static int FinalPlayerHP;
    public static int FinalEnemyHP;
    public static float FinalTime;

    public int playerHP = 100;
    public int enemyHP = 100;

    public Text playerHPText;
    public Text enemyHPText;


    private float elapsedTime;

    void Start()
    {
        UpdateHPUI();
        InvokeRepeating(nameof(HealEnemy), 15f, 15f);
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
       
    }
    public void AddPlayerHP(int value)
    {
        playerHP += value;
        UpdateHPUI();

        if (playerHP <= 0)
        {
            playerHP = 0; 

            FinalPlayerHP = playerHP;
            FinalEnemyHP = enemyHP;

            FinalTime = elapsedTime;  // ’Ç‰Á

            FindObjectOfType<SwordManager>()?.SaveFinalStatus();
            FindObjectOfType<ShieldManager>()?.SaveFinalStatus();

            SceneManager.LoadScene("GameOver");
        }
    }

    public void AddEnemyHP(int value)
    {
        enemyHP += value;
        UpdateHPUI();

        if (enemyHP <= 0)
        {
           
            enemyHP = 0;

            FinalPlayerHP = playerHP;
            FinalEnemyHP = enemyHP;
            FinalTime = elapsedTime;

            FindObjectOfType<SwordManager>()?.SaveFinalStatus();
            FindObjectOfType<ShieldManager>()?.SaveFinalStatus();

            CancelInvoke(nameof(HealEnemy));
            SceneManager.LoadScene("GameClear");
        }
    }

    void HealEnemy()
    {
        enemyHP += 5;
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        playerHPText.text = "HP : " + playerHP;
        enemyHPText.text = "HP : " + enemyHP;
    }
}