using System.Collections;
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

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip buttonSound;


    [Header("Damage Voice")]
    public AudioClip[] damageVoices;

    private int lastDamageVoice = -1;

    // 開始までの待機時間
    public float startDelay = 3f;

    // タイマー開始フラグ
    private bool isStarted = false;

    public Text finishText;

    private bool isFinished = false;
    void Start()
    {
        FinalPlayerHP = 0;
        FinalScore = 0;
        FinalTime = 0;
        DefultScore = 0;
        FinalRight = 0;
        FinalWorng = 0;
        FinalRW = 0;

        finishText.gameObject.SetActive(false);

        // 正解・不正解数をリセット
        Rightorwrong.Right = 0;
        Rightorwrong.Wrong = 0;

        playerStatusText.gameObject.SetActive(false);

        UpdateUI();

        Invoke(nameof(StartTimer), startDelay);
    }
    void PlayRandomDamageVoice()
    {
        if (audioSource == null || damageVoices == null || damageVoices.Length == 0)
            return;

        // 一定確率でのみ再生
        if (Random.value > 0.5f)
            return;

        int index;

        do
        {
            index = Random.Range(0, damageVoices.Length);
        }
        while (damageVoices.Length > 1 && index == lastDamageVoice);

        lastDamageVoice = index;

        audioSource.PlayOneShot(damageVoices[index]);
    }
    IEnumerator ResultScene()
    {
        isFinished = true;

        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }

        // ゲーム全体を停止
        Time.timeScale = 0f;

        finishText.gameObject.SetActive(true);

        // 実時間で3秒待つ
        yield return new WaitForSecondsRealtime(3f);

        // 次のシーンのために時間を元に戻す
        Time.timeScale = 1f;

        SceneManager.LoadScene("Result");
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
        if (!isStarted || isFinished)
            return;


        if (Quiz1.Boss == true)
            return;

        elapsedTime += Time.deltaTime;

        float remainTime = limitTime - elapsedTime;

        if (remainTime <= 0)
        {
            remainTime = 0;

            // HPボーナス
            score += playerHP * 10;

            FinalPlayerHP = playerHP;

            // 正解数・不正解数
            FinalRight = Rightorwrong.Right;
            FinalWorng = Rightorwrong.Wrong;
            FinalRW = (FinalRight - FinalWorng) * 10;

            // 正解・不正解がどちらも0ならスコア0
            if (FinalRight == 0 && FinalWorng == 0)
            {
                FinalScore = 0;
            }
            else
            {
                FinalScore = score + FinalRW;

                if (FinalScore < 0)
                    FinalScore = 0;
            }

            StartCoroutine(ResultScene());
        }

        int minutes = Mathf.FloorToInt(remainTime / 60);
        int seconds = Mathf.FloorToInt(remainTime % 60);

        timerText.text = $"残り時間 : {minutes}分{seconds}秒";
    }

    // プレイヤーHP増減
    public void AddPlayerHP(int value)
    {
        if (Quiz1.Boss == true)
            return;

        // ダメージを受けたとき
        if (value < 0)
        {
            PlayRandomDamageVoice();
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

            // 正解・不正解がどちらも0ならスコア0
            if (FinalRight == 0 && FinalWorng == 0)
            {
                FinalScore = 0;
            }
            else
            {
                FinalScore = score + FinalRW;

                if (FinalScore < 0)
                    FinalScore = 0;
            }

            StartCoroutine(ResultScene());
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

    // スコア減算
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