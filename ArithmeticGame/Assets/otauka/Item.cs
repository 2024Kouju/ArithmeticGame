using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Item : MonoBehaviour
{
    public GameObject panel;
    public QuizManager quizManager;

    // 表示までの時間
    public float interval = 0.5f;

    public void SetPanel(GameObject p)
    {
        panel = p;
    }

    // コルーチン開始
   

    void OnMouseDown()
    {

        StartCoroutine(OpenPanel());

       
    }

    IEnumerator OpenPanel()
    {
        // 待機
        yield return new WaitForSeconds(interval);

        // パネル表示
        panel.SetActive(true);

        // 問題表示
        if (quizManager != null)
        {
            quizManager.ShowRandomQuestion();
        }

        // このオブジェクト削除
        Destroy(gameObject);
    }
}
