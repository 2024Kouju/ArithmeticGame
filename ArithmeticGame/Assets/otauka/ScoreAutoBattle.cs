using UnityEngine;
using UnityEngine.UI;

public class ScoreAutoBattle : MonoBehaviour
{
    public ScoreAttackHP hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // 攻撃間隔
    public static float attackInterval = 10f;

    // 初期値
    private const float defaultAttackInterval = 10f;

    // 最低速度
    public float minAttackInterval = 3f;

    private float timer;

    // 攻撃ゲージ
    public Slider attackGauge;

    void Start()
    {
        attackInterval = defaultAttackInterval;

        attackGauge.maxValue = attackInterval;
        attackGauge.value = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        attackGauge.value = timer;

        if (timer >= attackInterval)
        {
            timer = 0f;

            PlayerAttack();
        }
    }

    void PlayerAttack()
    {
        int damage =
            swordManager.playerSword
            - shieldManager.enemyShield;

        if (damage < 1)
        {
            damage = 1;
        }

        hpManager.AddScore(damage);

        Debug.Log("自動攻撃！");
    }

    // 攻撃速度UP
    public void speedUpAttack(float value)
    {
        attackInterval -= value;

        if (attackInterval < minAttackInterval)
        {
            attackInterval = minAttackInterval;
        }

        attackGauge.maxValue = attackInterval;

        Debug.Log("攻撃速度UP！ 現在 : " + attackInterval);
    }

    // コンボ切れ時に呼ぶ
    public void ResetAttackSpeed()
    {
        attackInterval = defaultAttackInterval;

        attackGauge.maxValue = attackInterval;

        // ゲージもリセット
        timer = 0f;
        attackGauge.value = 0f;

        Debug.Log("攻撃速度リセット！");
    }
}