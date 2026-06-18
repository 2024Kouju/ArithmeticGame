using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public Text[] rankingTexts;

    void Start()
    {
        UpdateRankingDisplay();
    }

    void Update()
    {
        // Rキーを押したらランキングをリセット
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRanking();
        }
    }

    void UpdateRankingDisplay()
    {
        for (int i = 0; i < rankingTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("Rank" + i, 0);

            rankingTexts[i].text =
                (i + 1) + "位 : " + score + "点";
        }
    }

    void ResetRanking()
    {
        // 保存されているランキングデータを削除
        for (int i = 0; i < rankingTexts.Length; i++)
        {
            PlayerPrefs.DeleteKey("Rank" + i);
        }

        PlayerPrefs.Save();

        // 表示を更新
        UpdateRankingDisplay();

        Debug.Log("ランキングをリセットしました");
    }
}