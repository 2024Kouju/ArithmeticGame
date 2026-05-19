using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz1 : MonoBehaviour
{
    public List<QuestionData1> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData1 currentQuestion;

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

        questionText.text = currentQuestion.question1;

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

            txt.text = currentQuestion.choices1[i];

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void CheckAnswer(int index)
    {
        if (index == currentQuestion.correctIndex1)
        {
            Circle.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("正解！");
            if (Item1.HPFlag1 == true)
            {
                hpManager.AddPlayerHP(1);
                Item1.HPFlag1 = false;
            }
            else if (SwordItem1.SwordFlag1 == true)
            {
                swordManager.AddPlayerSword(1);
                SwordItem1.SwordFlag1 = false;
            }
            else if (ShieldItem1.ShieldFlag1 == true)
            {
                shieldManager.AddPlayerShield(1);
                ShieldItem1.ShieldFlag1 = false;
            }
        }
        else
        {
            Incorrect.SetActive(true);
            StartCoroutine(OpenPanel());
            Debug.Log("不正解！");
            if (Item1.HPFlag1 == true)
            {
                hpManager.AddEnemyHP(1);
                Item1.HPFlag1 = false;
            }
            else if (SwordItem1.SwordFlag1 == true)
            {
                swordManager.AddEnemySword(1);
                SwordItem1.SwordFlag1 = false;
            }
            else if (ShieldItem1.ShieldFlag1 == true)
            {
                shieldManager.AddEnemyShield(1);
                ShieldItem1.ShieldFlag1 = false;
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
public class QuestionData1
{
    [TextArea]
    public string question1;

    public string[] choices1 = new string[4];
    public int correctIndex1;
}