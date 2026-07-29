using System.Collections;
using UnityEngine;

public class Item100 : MonoBehaviour
{
    public GameObject quizPanel;
    public Quiz100 quizManager;

    public static bool HPFlag100 = false;

    // 表示までの時間
    public float interval = 0.5f;

    void OnMouseDown()
    {
        if (quizPanel.activeSelf)
        {
            return;
        }

        HPFlag100 = true;
        StartCoroutine(OpenPanel());
    }

    IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(interval);

        // 通常クイズを表示
        quizPanel.SetActive(true);

        if (quizManager != null)
        {
            quizManager.ShowRandomQuestion();
        }

        Destroy(gameObject);
    }
}