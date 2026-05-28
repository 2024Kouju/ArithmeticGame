using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;



public class SwordItem10 : MonoBehaviour
{
    public GameObject panel;
    public Quiz10 quizManager;

    static public bool SwordFlag10 = false;
    // 表示までの時間
    public float interval = 0.5f;

    // ジャンル選択


    public void SetPanel(GameObject p)
    {
        panel = p;
    }

    // コルーチン開始


    void OnMouseDown()
    {

        // panelが表示中ならクリック無効
        if (panel.activeSelf)
        {
            return;
        }
        SwordFlag10 = true;
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
