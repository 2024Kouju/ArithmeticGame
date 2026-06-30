using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

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
    public Text playerStatusText;
    public Text enemyStatusText;

    private float elapsedTime;

    // 開始フラグ
    private bool isStarted = false;

    // 停止時間
    public float startDelay = 3f;

    // 敵回復表示用キュー
    private Queue<int> enemyStatusQueue = new Queue<int>();
    private bool isShowingEnemyStatus = false;

    public VideoPlayer clearVideo;
    public GameObject videoImage;
    public VideoClip introClip;   // 最初だけ再生する動画
    public VideoClip loopClip;    // ループする動画


    private bool changedToLoop = false;
    // 最初の再生が終わったか
    private bool firstLoop = false;

    void Start()
    {
        UpdateHPUI();

        playerStatusText.gameObject.SetActive(false);
        enemyStatusText.gameObject.SetActive(false);

        videoImage.SetActive(false);

        clearVideo.loopPointReached += OnVideoFinished;

        Invoke(nameof(StartGame), startDelay);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if (!changedToLoop)
        {
            changedToLoop = true;

            vp.clip = loopClip;
            vp.isLooping = true;
            vp.Play();
        }
    }

    void StartGame()
    {
        isStarted = true;

        // 敵の自動回復開始
        InvokeRepeating(nameof(HealEnemy), 15f, 15f);
    }

    void Update()
    {
        if (!isStarted)
            return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = $"経過時間 : {minutes}分{seconds}秒";
    }

    // プレイヤー回復表示
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

    // 敵回復表示（順番待ち）
    void ShowEnemyStatus(int value)
    {
        enemyStatusQueue.Enqueue(value);

        if (!isShowingEnemyStatus)
        {
            StartCoroutine(ShowEnemyStatusQueue());
        }
    }

    IEnumerator ShowEnemyStatusQueue()
    {
        isShowingEnemyStatus = true;

        while (enemyStatusQueue.Count > 0)
        {
            int value = enemyStatusQueue.Dequeue();

            enemyStatusText.gameObject.SetActive(true);
            enemyStatusText.text = "HP +" + value;
            enemyStatusText.color = Color.green;

            // 1秒表示
            yield return new WaitForSeconds(1f);

            enemyStatusText.gameObject.SetActive(false);

            // 少し間を空ける
            yield return new WaitForSeconds(0.1f);
        }

        isShowingEnemyStatus = false;
    }

    public void AddPlayerHP(int value)
    {
        playerHP += value;

        if (value > 0)
        {
            ShowPlayerStatus(value);
        }

        if (playerHP <= 0)
        {
            playerHP = 0;
            UpdateHPUI();

            FinalPlayerHP = playerHP;
            FinalEnemyHP = enemyHP;
            FinalTime = elapsedTime;

            FindObjectOfType<SwordManager>()?.SaveFinalStatus();
            FindObjectOfType<ShieldManager>()?.SaveFinalStatus();

            SceneManager.LoadScene("GameOver");
            return;
        }

        UpdateHPUI();
    }

    public void AddEnemyHP(int value)
    {
        enemyHP += value;

        if (value > 0)
        {
            ShowEnemyStatus(value);
        }

        if (enemyHP <= 0)
        {
            enemyHP = 0;
            UpdateHPUI();

            FinalPlayerHP = playerHP;
            FinalEnemyHP = enemyHP;
            FinalTime = elapsedTime;

            FindObjectOfType<SwordManager>()?.SaveFinalStatus();
            FindObjectOfType<ShieldManager>()?.SaveFinalStatus();

            CancelInvoke(nameof(HealEnemy));

      

            // ゲーム停止
            CancelInvoke();
            Time.timeScale = 0;

            // 動画表示
            videoImage.SetActive(true);

            // 動画再生
            clearVideo.Play();

            return;
        }

        UpdateHPUI();
    }

    void HealEnemy()
    {
        enemyHP += 5;

        ShowEnemyStatus(5);

        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        playerHPText.text = "HP : " + playerHP;
        enemyHPText.text = "HP : " + enemyHP;
    }
}