using UnityEngine;
using UnityEngine.UI;

public class ScoreEnemyAuto : MonoBehaviour
{
    public ScoreAttackHP hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // 攻撃間隔
    public float attackInterval = 10f;

    private float timer;

    // 攻撃開始までの待機時間
    public float startDelay = 3f;

    // 攻撃開始フラグ
    private bool isStarted = false;

    // 敵攻撃ゲージ
    public Slider enemyAttackGauge;

    // ボイスSE
    public AudioSource audioSource;
    public AudioClip attackVoice;

    void Start()
    {
        // ゲージ最大値
        enemyAttackGauge.maxValue = attackInterval;

        // 初期値
        enemyAttackGauge.value = 0f;

        // 一定時間後に攻撃開始
        Invoke(nameof(StartEnemyAttack), startDelay);
    }

    void StartEnemyAttack()
    {
        isStarted = true;
    }

    void Update()
    {
        // 開始前は何もしない
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

            // ボイス再生
            audioSource.PlayOneShot(attackVoice);

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

        // プレイヤーHP減少
        hpManager.AddPlayerHP(-damage);

        Debug.Log("敵の自動攻撃！");
    }
}