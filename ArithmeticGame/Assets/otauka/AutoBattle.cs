using UnityEngine;
using UnityEngine.UI;

public class AutoBattle : MonoBehaviour
{
    public HPManager hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // UŒ‚ŠÔŠu
    public static float attackInterval = 10f;

    // ‰Šú’l
    private const float defaultAttackInterval = 10f;

    // Å’á‘¬“x
    public float minAttackInterval = 3f;

    private float timer;

    // UŒ‚ƒQ[ƒW
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

        hpManager.AddEnemyHP(-damage);

        Debug.Log("Ž©“®UŒ‚I");
    }

    // UŒ‚‘¬“xUP
    public void SpeedUpAttack(float value)
    {
        attackInterval -= value;

        if (attackInterval < minAttackInterval)
        {
            attackInterval = minAttackInterval;
        }

        attackGauge.maxValue = attackInterval;

        Debug.Log("UŒ‚‘¬“xUPI Œ»Ý:" + attackInterval);
    }

    // ƒRƒ“ƒ{Ø‚êŽž‚ÉŒÄ‚Ô
    public void ResetAttackSpeed()
    {
        attackInterval = defaultAttackInterval;

        timer = 0f;

        attackGauge.maxValue = attackInterval;
        attackGauge.value = 0f;

        Debug.Log("UŒ‚‘¬“xƒŠƒZƒbƒgI");
    }
}