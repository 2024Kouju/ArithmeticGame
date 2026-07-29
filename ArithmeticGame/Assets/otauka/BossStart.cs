using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossStart : MonoBehaviour
{
    public Text messageText;   // メッセージ用
    public Text countText;     // カウント用

    IEnumerator Start()
    {
        // メッセージ表示
        messageText.text = "授業を開始します。";
        countText.text = "";

        yield return new WaitForSeconds(2f);

        // メッセージを消す
        messageText.text = "";

        // カウントダウン
        for (int i = 3; i >= 0; i--)
        {
            countText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // カウントを消す
        countText.text = "";
    }
}