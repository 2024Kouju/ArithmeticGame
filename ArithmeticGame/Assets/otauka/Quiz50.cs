using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz50 : MonoBehaviour
{
    public List<QuestionData50> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData50 currentQuestion;

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

        questionText.text = currentQuestion.question50;

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

            txt.text = currentQuestion.choices50[i];

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctIndex50)
        {
            Circle.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("正解！");
            if (Item50.HPFlag50 == true)
            {
                hpManager.AddPlayerHP(50);
                Item50.HPFlag50 = false;
            }
            else if (SwordItem50.SwordFlag50 == true)
            {
                swordManager.AddPlayerSword(50);
                SwordItem50.SwordFlag50 = false;
            }
            else if (ShieldItem50.ShieldFlag50 == true)
            {
                shieldManager.AddPlayerShield(50);
                ShieldItem50.ShieldFlag50 = false;
            }

        }
        else
        {
            Incorrect.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("不正解！");

            if (Item50.HPFlag50 == true)
            {
                hpManager.AddEnemyHP(50);
                Item50.HPFlag50 = false;
            }
            else if (SwordItem50.SwordFlag50 == true)
            {
                swordManager.AddEnemySword(50);
                SwordItem50.SwordFlag50 = false;
            }
            else if (ShieldItem50.ShieldFlag50 == true)
            {
                shieldManager.AddEnemyShield(50);
                ShieldItem50.ShieldFlag50 = false;
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
public class QuestionData50
{
    [TextArea]
    public string question50;

    public string[] choices50 = new string[4];
    public int correctIndex50;
}