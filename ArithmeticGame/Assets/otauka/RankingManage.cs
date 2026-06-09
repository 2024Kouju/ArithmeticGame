using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public Text[] rankingTexts;

    void Start()
    {
        for (int i = 0; i < rankingTexts.Length; i++)
        {
            int score = PlayerPrefs.GetInt("Rank" + i, 0);

            rankingTexts[i].text =
                (i + 1) + "ˆÊ : " + score + "“_";
        }
    }
}