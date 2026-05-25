using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz100 : MonoBehaviour
{
    public List<QuestionData100> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData100 currentQuestion;

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

        questionText.text = currentQuestion.question100;

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

            txt.text = currentQuestion.choices100[i];

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctIndex100)
        {
            Circle.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("正解！");
            if (Item100.HPFlag100 == true)
            {
                hpManager.AddPlayerHP(100);
                Item100.HPFlag100 = false;
            }
            else if (SwordItem100.SwordFlag100 == true)
            {
                swordManager.AddPlayerSword(100);
                SwordItem100.SwordFlag100 = false;
            }
            else if (ShieldItem100.ShieldFlag100 == true)
            {
                shieldManager.AddPlayerShield(100);
                ShieldItem100.ShieldFlag100 = false;
            }
        }
        else
        {
            Incorrect.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("不正解！");
            if (Item100.HPFlag100 == true)
            {
                hpManager.AddEnemyHP(100);
                Item100.HPFlag100 = false;
            }
            else if (SwordItem100.SwordFlag100 == true)
            {
                swordManager.AddEnemySword(100);
                SwordItem100.SwordFlag100 = false;
            }
            else if (ShieldItem100.ShieldFlag100 == true)
            {
                shieldManager.AddEnemyShield(100);
                ShieldItem100.ShieldFlag100 = false;
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
public class QuestionData100
{
    [TextArea]
    public string question100;

    public string[] choices100 = new string[4];
    public int correctIndex100;
}