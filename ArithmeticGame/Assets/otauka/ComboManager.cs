using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // 正解時（通常バトル）
    public void AddCombo()
    {
        comboCount++;

        UpdateComboUI();

        Debug.Log("現在コンボ : " + comboCount);

        // 5コンボごとに攻撃速度UP
        if (comboCount % 5 == 0)
        {
            if (autoBattle != null)
            {
                autoBattle.SpeedUpAttack(1f);
            }
        }
    }

    // 正解時（スコアアタック）
    public void AddScoreCombo()
    {
        comboCount++;

        UpdateComboUI();

        Debug.Log("現在コンボ : " + comboCount);

        // 5コンボごとに攻撃速度UP
        if (comboCount % 5 == 0)
        {
            if (scoreautoBattle != null)
            {
                scoreautoBattle.speedUpAttack(1f);
            }
        }
    }

    // ミス時
    public void ResetCombo()
    {
        comboCount = 0;

        UpdateComboUI();

        if (autoBattle != null)
        {
            autoBattle.ResetAttackSpeed();
        }

        if (scoreautoBattle != null)
        {
            scoreautoBattle.ResetAttackSpeed();
        }

        Debug.Log("コンボリセット");
    }
    void UpdateComboUI()
    {
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