using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreAttackHP : MonoBehaviour
{
    public static int FinalPlayerHP;
    public static int FinalScore;
    public static float FinalTime;
    public static int DefultScore;

    public int playerHP = 100;
    public int score = 0;

    public float limitTime = 60f;

    public Text playerHPText;
    public Text scoreText;
    public Text timerText;

    private float elapsedTime;

    public static int FinalRight;
    public static int FinalWorng;
    public static int FinalRW;
    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Quiz1.Boss == true)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        float remainTime = limitTime - elapsedTime;

        if (remainTime <= 0)
        {
            remainTime = 0;


            score += playerHP * 10;
            FinalPlayerHP = playerHP;
            FinalRight = Rightorwrong.Right * 10;
            FinalWorng = Rightorwrong.Wrong * 10;

            FinalRW = FinalRight - FinalWorng;

            if(FinalRW < 0)
            {
                FinalRW = 0;
            }

            FinalScore = score + FinalRW;

            SceneManager.LoadScene("Result");
        }

        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);

        timerText.text = $"残り時間 : {minutes}分{seconds}秒";
    }

    // プレイヤーHP増減
    public void AddPlayerHP(int value)
    {
        if(Quiz1.Boss == true)
        {
            return;
        }

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
        DefultScore += value;

        if (score < 0)
            score = 0;

        UpdateUI();
    }

    void UpdateUI()
    {
        playerHPText.text = "HP : " + playerHP;
        scoreText.text = "スコア : " + score;
    }
}