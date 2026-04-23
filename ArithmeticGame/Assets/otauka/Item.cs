using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Item : MonoBehaviour
{
    public GameObject panel;
    public QuizManager quizManager;

    public void SetPanel(GameObject p)
    {
        panel = p;
    }

    void OnMouseDown()
    {
        // パネル表示
        panel.SetActive(true);

        // ★ここで問題を出す
        if (quizManager != null)
        {
            quizManager.ShowRandomQuestion();
        }
    }
}
