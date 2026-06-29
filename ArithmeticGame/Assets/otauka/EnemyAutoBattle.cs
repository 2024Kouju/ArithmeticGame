using UnityEngine;
using UnityEngine.UI;

public class EnemyAutoBattle : MonoBehaviour
{
    public HPManager hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // ボイスSE
    public AudioSource audioSource;
    public AudioClip attackVoice;

    // 攻撃間隔
    public float attackInterval = 10f;

    private float timer;

    // 開始までの待機時間
    public float startDelay = 3f;

    // 開始フラグ
    private bool isStarted = false;


    // 敵攻撃ゲージ
    public Slider enemyAttackGauge;

    void Start()
    {
        // ゲージ最大値
        enemyAttackGauge.maxValue = attackInterval;

        // 初期値
        enemyAttackGauge.value = 0;
        // 3秒後に攻撃開始
        Invoke(nameof(StartEnemyBattle), startDelay);
    }

    void StartEnemyBattle()
    {
        isStarted = true;
    }
    void Update()
    {
        // 開始前は停止
        if (!isStarted)
        {
            return;
        }

        timer += Time.deltaTime;

        // ゲージ更新
        enemyAttackGauge.value = timer;

        // 時間になったら攻撃
        if (timer >= attackInterval)
        {
            timer = 0f;

            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        int damage =
            swordManager.enemySword
            - shieldManager.playerShield;

        // 最低ダメージ1
        if (damage < 1)
        {
            damage = 1;
        }
        // ボイス再生
        audioSource.PlayOneShot(attackVoice);
        // プレイヤーHP減少
        hpManager.AddPlayerHP(-damage);

        Debug.Log("敵の自動攻撃！");
    }
}
