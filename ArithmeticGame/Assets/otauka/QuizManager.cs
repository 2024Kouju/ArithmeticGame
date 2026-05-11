using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestionData> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData currentQuestion;

    public GameObject panel;

    public void ShowRandomQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("questionsÇ™ãÛ");
            return;
        }

        currentQuestion = questions[Random.Range(0, questions.Count)];

        if (questionText == null)
        {
            Debug.LogError("questionTextñ¢ê›íË");
            return;
        }

        questionText.text = currentQuestion.question;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;

            TextMeshProUGUI txt =
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (txt == null)
            {
                Debug.LogError("ButtonÇ…TMPÇ™Ç»Ç¢: " + i);
                continue;
            }

            txt.text = currentQuestion.choices[i];

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctIndex)
        {
            panel.SetActive(false);
            Debug.Log("ê≥âÅI");

        }
        else
        {
            panel.SetActive(false);
            Debug.Log("ïsê≥âÅI");
        }
    }
}

[System.Serializable]
public class QuestionData
{
    [TextArea]
    public string question;

    public string[] choices = new string[4];
    public int correctIndex;
}