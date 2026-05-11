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

            StartCoroutine(OpenPanel());
            Debug.Log("正解！");

        }
        else
        {
            StartCoroutine(OpenPanel());
            Debug.Log("不正解！");
        }
    }

    IEnumerator OpenPanel()
    {
        // 待機
        yield return new WaitForSeconds(interval);

        // パネル表示
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