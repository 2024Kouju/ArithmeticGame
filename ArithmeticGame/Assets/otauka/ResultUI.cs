using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    public Text hpText;
    public Text swordText;
    public Text shieldText;

    public Text EnemyHPText;
    public Text EnemySwordText;
    public Text EnemyShieldText;

    public Text GameTimeText;

    void Start()
    {
        hpText.text = "HP : " + HPManager.FinalPlayerHP;
        swordText.text = "çUåÇóÕ : " + SwordManager.FinalPlayerSword;
        shieldText.text = "ñhå‰óÕ : " + ShieldManager.FinalPlayerShield;

        EnemyHPText.text = "HP : " + HPManager.FinalEnemyHP;
        EnemySwordText.text = "çUåÇóÕ : " + SwordManager.FinalEnemySword;
        EnemyShieldText.text = "ñhå‰óÕ : " + ShieldManager.FinalEnemyShield;
        int minutes = Mathf.FloorToInt(HPManager.FinalTime / 60);
        int seconds = Mathf.FloorToInt(HPManager.FinalTime % 60);

        GameTimeText.text = $"É^ÉCÉÄ : {minutes:00}ï™{seconds:00}ïb";
    }
}
