using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz10 : MonoBehaviour
{
    public List<QuestionData10> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData10 currentQuestion;

    public GameObject panel;

    public HPManager hpManager;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    // 表示までの時間
    public float interval = 0.5f;


    public void ShowRandomQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("questionsが空");
            return;
        }

        currentQuestion = questions[Random.Range(0, questions.Count)];

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question10;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;

            TextMeshProUGUI txt =
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (txt == null)
            {
                Debug.LogError("ButtonにTMPがない: " + i);
                continue;
            }

            txt.text = currentQuestion.choices10[i];

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctIndex10)
        {
            Circle.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("正解！");
            if (Item10.HPFlag10 == true)
            {
                hpManager.AddPlayerHP(10);
                Item10.HPFlag10 = false;
            }
            else if (SwordItem10.SwordFlag10 == true)
            {
                swordManager.AddPlayerSword(10);
                SwordItem10.SwordFlag10 = false;
            }
            else if (ShieldItem10.ShieldFlag10 == true)
            {
                shieldManager.AddPlayerShield(10);
                ShieldItem10.ShieldFlag10 = false;
            }
        }
        else
        {
            Incorrect.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("不正解！");
            if (Item10.HPFlag10 == true)
            {
                hpManager.AddEnemyHP(10);
                Item10.HPFlag10 = false;
            }
            else if (SwordItem10.SwordFlag10 == true)
            {
                swordManager.AddEnemySword(10);
                SwordItem10.SwordFlag10 = false;
            }
            else if (ShieldItem10.ShieldFlag10 == true)
            {
                shieldManager.AddEnemyShield(10);
                ShieldItem10.ShieldFlag10 = false;
            }
        }


    }

    IEnumerator OpenPanel()
    {
        // 待機
        yield return new WaitForSeconds(interval);

        Circle.SetActive(false);
        Incorrect.SetActive(false);
        panel.SetActive(false);



    }


}



[System.Serializable]
public class QuestionData10
{
    [TextArea]
    public string question10;

    public string[] choices10 = new string[4];
    public int correctIndex10;
}