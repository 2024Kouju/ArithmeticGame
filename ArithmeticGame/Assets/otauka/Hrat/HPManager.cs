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
    public Text timerText;

    private float elapsedTime;

    // ŠJŽnƒtƒ‰ƒO
    private bool isStarted = false;

    // ’âŽ~ŽžŠÔ
    public float startDelay = 3f;

    void Start()
    {
        UpdateHPUI();

        // 3•bŒã‚ÉŠJŽn
        Invoke(nameof(StartGame), startDelay);
    }

    void StartGame()
    {
        isStarted = true;

        // “G‚Ì‰ñ•œ‚à‚±‚±‚©‚çŠJŽn
        InvokeRepeating(nameof(HealEnemy), 15f, 15f);
    }

    void Update()
    {
        // 3•bŠÔ’âŽ~
        if (!isStarted)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = $"Œo‰ßŽžŠÔ : {minutes}•ª{seconds}•b";
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
            FinalTime = elapsedTime;

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