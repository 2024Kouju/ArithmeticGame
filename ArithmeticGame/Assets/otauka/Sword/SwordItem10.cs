using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;



public class SwordItem10 : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject imageQuizPanel;

    public Quiz10 quizManager;
    public imageQuiz10 imageQuizManager;
    static public bool SwordFlag10 = false;

    // 表示までの時間
    public float interval = 0.5f;

    // ジャンル選択



    // コルーチン開始


    void OnMouseDown()
    {
        if (quizPanel.activeSelf || imageQuizPanel.activeSelf)
        {
            return;
        }
        SwordFlag10 = true;
        StartCoroutine(OpenPanel());


    }

    IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(interval);

        int rand = Random.Range(0, 100);

        if (rand < 5)
        {
            //画像クイズ
            imageQuizPanel.SetActive(true);

            if (imageQuizManager != null)
            {
                imageQuizManager.ShowRandomQuestion();
            }
        }
        else
        {
            // 通常クイズ
            quizPanel.SetActive(true);

            if (quizManager != null)
            {
                quizManager.ShowRandomQuestion();
            }
        }

        Destroy(gameObject);
    }
}
