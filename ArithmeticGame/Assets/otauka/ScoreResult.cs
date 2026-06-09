using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    public Text hpText;

    void Start()
    {
        int score = ScoreAttackHP.FinalScore;

        hpText.text = "スコア : " + score + "点";

        SaveRanking(score);
    }

    void SaveRanking(int score)
    {
        int[] ranking = new int[5];

        // 保存済みランキング取得
        for (int i = 0; i < 5; i++)
        {
            ranking[i] = PlayerPrefs.GetInt("Rank" + i, 0);
        }

        // 新スコア挿入
        for (int i = 0; i < 5; i++)
        {
            if (score > ranking[i])
            {
                for (int j = 4; j > i; j--)
                {
                    ranking[j] = ranking[j - 1];
                }

                ranking[i] = score;
                break;
            }
        }

        // 保存
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Rank" + i, ranking[i]);
        }

        PlayerPrefs.Save();
    }
}