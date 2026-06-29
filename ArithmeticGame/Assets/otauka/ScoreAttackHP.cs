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
    public Text playerStatusText;
    private float elapsedTime;

    public static int FinalRight;
    public static int FinalWorng;
    public static int FinalRW;

    // 開始までの待機時間
    public float startDelay = 3f;

    // タイマー開始フラグ
    private bool isStarted = false;
    void Start()
    {
        FinalPlayerHP = 0;
        FinalScore = 0;
        FinalTime = 0;
        DefultScore = 0;
        FinalRight = 0;
        FinalWorng = 0;
        FinalRW = 0;

        playerStatusText.gameObject.SetActive(false);

        UpdateUI();

        Invoke(nameof(StartTimer), startDelay);
    }

    void StartTimer()
    {
        isStarted = true;
    }
    // HP回復表示
    void ShowPlayerStatus(int value)
    {
        playerStatusText.gameObject.SetActive(true);
        playerStatusText.text = "HP +" + value;
        playerStatusText.color = Color.green;

        CancelInvoke(nameof(HidePlayerStatus));
        Invoke(nameof(HidePlayerStatus), 1f);
    }

    void HidePlayerStatus()
    {
        playerStatusText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (!isStarted)
        {
            return;
        }

        if (Quiz1.Boss == true)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        float remainTime = limitTime - elapsedTime;


        if (remainTime <= 0)
        {
            remainTime = 0;

            // HPボーナス
            score += playerHP * 10;

            FinalPlayerHP = playerHP;

            // 正解数・不正解数を保存
            FinalRight = Rightorwrong.Right;
            FinalWorng = Rightorwrong.Wrong;

            // (正解数 - 不正解数) × 10
            FinalRW = (FinalRight - FinalWorng) * 10;

            // 最終スコア
            FinalScore = score + FinalRW;

            // マイナスにならないようにする
            if (FinalScore < 0)
            {
                FinalScore = 0;
            }

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

        if (value > 0)
        {
            ShowPlayerStatus(value);
        }

        if (playerHP <= 0)
        {
            playerHP = 0;

            FinalPlayerHP = playerHP;

            FinalRight = Rightorwrong.Right;
            FinalWorng = Rightorwrong.Wrong;
            FinalRW = (FinalRight - FinalWorng) * 10;

            FinalScore = score + FinalRW;

            if (FinalScore < 0)
            {
                FinalScore = 0;
            }

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
    public void SubScore(int value)
    {
        score -= value;
        DefultScore -= value;

        if (score < 0)
        {
            score = 0;
            DefultScore = 0;
        }
           

        UpdateUI();
    }
    void UpdateUI()
    {
        playerHPText.text = "HP : " + playerHP;
        scoreText.text = "スコア : " + score;
    }
}