using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz5 : MonoBehaviour
{
    public List<QuestionData5> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData5 currentQuestion;

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

        questionText.text = currentQuestion.question5;

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

            txt.text = currentQuestion.choices5[i];

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctIndex5)
        {
            Circle.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("正解！");
            if (Item5.HPFlag5 == true)
            {
                hpManager.AddPlayerHP(5);
                Item5.HPFlag5 = false;
            }
            else if (SwordItem5.SwordFlag5 == true)
            {
                swordManager.AddPlayerSword(5);
                SwordItem5.SwordFlag5 = false;
            }
            else if (ShieldItem5.ShieldFlag5 == true)
            {
                shieldManager.AddPlayerShield(5);
                ShieldItem5.ShieldFlag5 = false;
            }

        }
        else
        {
            Incorrect.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("不正解！");
         
            if (Item5.HPFlag5 == true)
            {
                hpManager.AddEnemyHP(5);
                Item5.HPFlag5 = false;
            }
            else if (SwordItem5.SwordFlag5 == true)
            {
                swordManager.AddEnemySword(5);
                SwordItem5.SwordFlag5 = false;
            }
            else if (ShieldItem5.ShieldFlag5 == true)
            {
                shieldManager.AddEnemyShield(5);
                ShieldItem5.ShieldFlag5 = false;
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
public class QuestionData5
{
    [TextArea]
    public string question5;

    public string[] choices5 = new string[4];
    public int correctIndex5;
}