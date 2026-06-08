using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreAttackHP : MonoBehaviour
{
    public static int FinalPlayerHP;
    public static int FinalScore;
    public static float FinalTime;

    public int playerHP = 100;
    public int score = 0;

    public float limitTime = 60f;

    public Text playerHPText;
    public Text scoreText;
    public Text timerText;

    private float elapsedTime;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float remainTime = limitTime - elapsedTime;

        if (remainTime <= 0)
        {
            playerHP = 0;

            score += playerHP * 10;

            FinalPlayerHP = playerHP;
            FinalScore = score;

            SceneManager.LoadScene("Result");
        }

        timerText.text = "TIME : " + remainTime.ToString("F1");
    }

    // プレイヤーHP増減
    public void AddPlayerHP(int value)
    {
        playerHP += value;

        if (playerHP <= 0)
        {
            playerHP = 0;

           

            FinalPlayerHP = playerHP;
            FinalScore = score;
         


            SceneManager.LoadScene("Result");
        }

        UpdateUI();
    }

    // スコア加算
    public void AddScore(int value)
    {
        score += value;

        if (score < 0)
            score = 0;

        UpdateUI();
    }

    void UpdateUI()
    {
        playerHPText.text = "HP : " + playerHP;
        scoreText.text = "SCORE : " + score;
    }
}