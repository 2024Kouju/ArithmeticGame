using UnityEngine;
using UnityEngine.UI;

public class AutoBattle : MonoBehaviour
{
    public HPManager hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // UŒ‚ŠÔŠu
    public float attackInterval = 10f;

    private float timer;

    // UŒ‚ƒQ[ƒW
    public Slider attackGauge;

    void Start()
    {
        // ƒQ[ƒWÅ‘å’l
        attackGauge.maxValue = attackInterval;

        // ‰Šú’l
        attackGauge.value = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ƒQ[ƒWXV
        attackGauge.value = timer;

        // ŽžŠÔ‚É‚È‚Á‚½‚çUŒ‚
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
}