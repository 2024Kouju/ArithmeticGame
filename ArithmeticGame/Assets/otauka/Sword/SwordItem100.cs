using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;



public class SwordItem100 : MonoBehaviour
{
    public GameObject quizPanel;

    public Quiz100 quizManager;

    static public bool SwordFlag100 = false;

    // 表示までの時間
    public float interval = 0.5f;

    // ジャンル選択



    // コルーチン開始


    void OnMouseDown()
    {
        if (quizPanel.activeSelf)
        {
            return;
        }
        SwordFlag100 = true;
        StartCoroutine(OpenPanel());


    }

    IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(interval);

 
       
            // 通常クイズ
            quizPanel.SetActive(true);

            if (quizManager != null)
            {
                quizManager.ShowRandomQuestion();
            }
        

        Destroy(gameObject);
    }
}
