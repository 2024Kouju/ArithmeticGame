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

    // 動画
    public VideoPlayer clearVideo;
    public GameObject videoImage;

    public VideoClip winVideo;
    public VideoClip loseVideo;

    // 遷移先シーン
    public string winScene = "Result";
    public string loseScene = "GameOver";

    // 勝敗判定
    private bool isWin = false;

    // HP30以下警告音
    public AudioSource audioSource;
    public AudioClip lowHPVoice;

    // 一度だけ再生するため
    private bool isLowHPVoicePlayed = false;

    // 敵HP低下ボイス
    public AudioClip enemyLowHPVoice;

    // 一度だけ再生するため
    private bool isEnemyLowHPVoicePlayed = false;


    public Animator enemyAnimator;
    void Start()
    {
        UpdateHPUI();

        playerStatusText.gameObject.SetActive(false);
        enemyStatusText.gameObject.SetActive(false);

        videoImage.SetActive(false);

        clearVideo.loopPointReached += OnVideoFinished;

        Invoke(nameof(StartGame), startDelay);
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

    // 動画終了時
    void OnVideoFinished(VideoPlayer vp)
    {
        Time.timeScale = 1;

        if (isWin)
        {
            SceneManager.LoadScene(winScene);
        }
        else
        {
            SceneManager.LoadScene(loseScene);
        }
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

    // 敵回復表示
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

            yield return new WaitForSeconds(1f);

            enemyStatusText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

        isShowingEnemyStatus = false;
    }

    public void AddPlayerHP(int value)
    {
       

        playerHP += value;


        // HP30以下で一度だけ再生
        if (playerHP <= 30 && playerHP > 0 && !isLowHPVoicePlayed)
        {
            audioSource.PlayOneShot(lowHPVoice);
            isLowHPVoicePlayed = true;
        }

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

            CancelInvoke(nameof(HealEnemy));

            StartCoroutine(PlayerLoseSequence());

            return;
        }

        UpdateHPUI();
    }
    IEnumerator PlayerLoseSequence()
    {
        isStarted = false;

       

        // アニメーション時間
        yield return new WaitForSeconds(1.5f);

        Time.timeScale = 0;

        isWin = false;

        videoImage.SetActive(true);

        clearVideo.clip = loseVideo;
        clearVideo.Play();
    }
    IEnumerator EnemyLoseSequence()
    {
        isStarted = false;

        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Lose");
        }

        yield return new WaitForSeconds(3.0f);

        Time.timeScale = 0;

        isWin = true;

        videoImage.SetActive(true);

        clearVideo.clip = winVideo;
        clearVideo.Play();
    }

    public void AddEnemyHP(int value)
    {
      
        enemyHP += value;

        // 敵HP30以下で一度だけ再生
        if (enemyHP <= 30 && enemyHP > 0 && !isEnemyLowHPVoicePlayed)
        {
            audioSource.PlayOneShot(enemyLowHPVoice);
            isEnemyLowHPVoicePlayed = true;
        }

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

            StartCoroutine(EnemyLoseSequence());

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

    private void OnDestroy()
    {
        if (clearVideo != null)
        {
            clearVideo.loopPointReached -= OnVideoFinished;
        }
    }
}