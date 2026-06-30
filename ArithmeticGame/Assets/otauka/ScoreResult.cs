using UnityEngine;
using UnityEngine.UI;

public class ScoreResult : MonoBehaviour
{
    public Text hpText;
    public Text Addition;
    public Text Defult;
    public Text right;
    public Text wrong;

    // 追加
    public Image noResultImage;

    void Start()
    {
        int score = ScoreAttackHP.FinalScore;
        int PlayerHP = ScoreAttackHP.FinalPlayerHP;
        int Defultscore = ScoreAttackHP.DefultScore;
        int Right = ScoreAttackHP.FinalRight;
        int Wrong = ScoreAttackHP.FinalWorng;

      

        // 正解・不正解がどちらも0か判定
        if (Right == 0 && Wrong == 0)
        {
            // Textを非表示
            right.gameObject.SetActive(false);
            wrong.gameObject.SetActive(false);
            Defult.gameObject.SetActive(false);
            Addition.gameObject.SetActive(false);

            // 画像を表示
            noResultImage.gameObject.SetActive(true);
        }
        else
        {
            // Textを表示
            right.gameObject.SetActive(true);
            wrong.gameObject.SetActive(true);
            Defult.gameObject.SetActive(true);
            Addition.gameObject.SetActive(true);

            Defult.text = "スコア : " + Defultscore + "点";
            Addition.text = "+" + "残りHP :" + PlayerHP + "× 10";
            right.text = "+" + "正解数:" + Right + "× 10";
            wrong.text = "-" + "不正解数:" + Wrong + "× 10";

            // 画像を非表示
            noResultImage.gameObject.SetActive(false);
        }

        hpText.text = "合計スコア : " + score + "点";

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