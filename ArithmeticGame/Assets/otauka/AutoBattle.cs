using UnityEngine;
using UnityEngine.UI;

public class AutoBattle : MonoBehaviour
{
    public HPManager hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // UŒ‚ŠÔŠu
    public float attackInterval = 10f;

    // Å’á‘¬“x
    public float minAttackInterval = 0f;

    private float timer;

    // UŒ‚ƒQ[ƒW
    public Slider attackGauge;

    void Start()
    {
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

    // š’Ç‰Á
    public void SpeedUpAttack(float value)
    {
        attackInterval -= value;

        // ‘¬‚­‚È‚è‚·‚¬–hŽ~
        if (attackInterval < minAttackInterval)
        {
            attackInterval = minAttackInterval;
        }

        // ƒQ[ƒWÅ‘å’lXV
        attackGauge.maxValue = attackInterval;

        Debug.Log("UŒ‚‘¬“xUPI Œ»Ý:" + attackInterval);
    }
}