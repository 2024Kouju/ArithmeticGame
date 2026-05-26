using UnityEngine;
using UnityEngine.UI;

public class EnemyAutoBattle : MonoBehaviour
{
    public HPManager hpManager;
    public SwordManager swordManager;
    public ShieldManager shieldManager;

    // UŒ‚ŠÔŠu
    public float attackInterval = 10f;

    private float timer;

    // “GUŒ‚ƒQ[ƒW
    public Slider enemyAttackGauge;

    void Start()
    {
        // ƒQ[ƒWÅ‘å’l
        enemyAttackGauge.maxValue = attackInterval;

        // ‰Šú’l
        enemyAttackGauge.value = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ƒQ[ƒWXV
        enemyAttackGauge.value = timer;

        // ŠÔ‚É‚È‚Á‚½‚çUŒ‚
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

        // Å’áƒ_ƒ[ƒW1
        if (damage < 1)
        {
            damage = 1;
        }

        // ƒvƒŒƒCƒ„[HPŒ¸­
        hpManager.AddPlayerHP(-damage);

        Debug.Log("“G‚Ì©“®UŒ‚I");
    }
}
