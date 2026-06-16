using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreStart : MonoBehaviour
{
    public Text startText;

    IEnumerator Start()
    {
        // 2秒間メッセージ表示
        startText.text = "テストを開始します。";
        yield return new WaitForSeconds(2f);

        // 3カウント
        for (int i = 3; i >= 0; i--)
        {
            startText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // 消す
        startText.text = "";
        // または
        // startText.gameObject.SetActive(false);
    }
}