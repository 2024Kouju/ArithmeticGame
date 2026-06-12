using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    // 現在コンボ
    public int comboCount = 0;

    // コンボ表示
    public Text comboText;

    // AutoBattle参照
    public AutoBattle autoBattle;

    public ScoreAutoBattle scoreautoBattle;

   
    void Start()
    {
        UpdateComboUI();
    }

    // 正解時
    public void AddCombo()
    {


        comboCount++;

        UpdateComboUI();

        Debug.Log("現在コンボ : " + comboCount);

        // 5コンボごと
        if (comboCount % 5 == 0)
        {
            // 0.5秒速くする
            autoBattle.SpeedUpAttack(1f);
        }
    }

    public void AddScoreCombo()
    {


        comboCount++;

        UpdateComboUI();

        Debug.Log("現在コンボ : " + comboCount);

        // 5コンボごと
        if (comboCount % 5 == 0)
        {
            // 0.5秒速くする
            scoreautoBattle.speedUpAttack(1f);
        }
    }

    // ミス時
    public void ResetCombo()
    {
        comboCount = 0;

     

        UpdateComboUI();

        AutoBattle.attackInterval = 10f;

        Debug.Log("コンボリセット");
    }

    void UpdateComboUI()
    {
        // 2コンボ以上で表示
        if (comboCount >= 2)
        {
            comboText.text = comboCount + " コンボ";
        }
        else
        {
            comboText.text = "";
        }
    }
}
