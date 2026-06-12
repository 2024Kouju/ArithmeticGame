using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Quiz10 : MonoBehaviour
{
    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<QuestionData10> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData10 currentQuestion;

    public GameObject panel;

    public HPManager hpManager;

    public ScoreAttackHP attackHP;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    public QuestionCount questionCount;

    public ComboManager comboManager;

    static public bool Score10 = false;

    static public bool Boss10 = false;

    // 表示までの時間
    public float interval = 0.5f;

    // シャッフル後の選択肢
    private List<ChoiceData10> shuffledChoices = new List<ChoiceData10>();

    public void ShowRandomQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("questionsが空");
            return;
        }

        // 問題数を増やす
        questionCount.AddQuestion(1);

        currentQuestion = questions[Random.Range(0, questions.Count)];

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question10;

        // 選択肢を作成
        shuffledChoices.Clear();

        for (int i = 0; i < currentQuestion.choices10.Length; i++)
        {
            ChoiceData10 data = new ChoiceData10();

            data.choiceText = currentQuestion.choices10[i];

            data.isCorrect = (i == currentQuestion.correctIndex10);

            shuffledChoices.Add(data);
        }

        // シャッフル
        ShuffleChoices();

        // ボタンに設定
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

            txt.text = shuffledChoices[i].choiceText;

            choiceButtons[i].onClick.RemoveAllListeners();

            choiceButtons[i].onClick.AddListener(() =>
            {
                CheckAnswer(index);
            });
        }
    }

    void ShuffleChoices()
    {
        for (int i = 0; i < shuffledChoices.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledChoices.Count);

            ChoiceData10 temp = shuffledChoices[i];
            shuffledChoices[i] = shuffledChoices[randomIndex];
            shuffledChoices[randomIndex] = temp;
        }
    }

    void CheckAnswer(int index)
    {
        if (shuffledChoices[index].isCorrect)
        {
            CirclesoundEffect.Play();

            Circle.SetActive(true);

            StartCoroutine(OpenPanel());

     

            Debug.Log("正解！");

            if (Boss10 == true)
            {
                comboManager.AddCombo();
                // アイテム効果
                if (Item10.HPFlag10 == true)
                {
                    hpManager.AddPlayerHP(10);
                    QuizUnlockManager.Heart10Clear = true;
                    Item10.HPFlag10 = false;
                }
                else if (SwordItem10.SwordFlag10 == true)
                {
                    swordManager.AddPlayerSword(10);
                    QuizUnlockManager.Sword10Clear = true;
                    SwordItem10.SwordFlag10 = false;
                }
                else if (ShieldItem10.ShieldFlag10 == true)
                {
                    shieldManager.AddPlayerShield(10);
                    QuizUnlockManager.Shield10Clear = true;
                    ShieldItem10.ShieldFlag10 = false;
                }
            }

            if (Score10 == true)
            {
                comboManager.AddScoreCombo();
                // アイテム効果
                if (Item10.HPFlag10 == true)
                {
                    attackHP.AddPlayerHP(10);
                    QuizUnlockManager.Heart10Clear = true;
                    Item10.HPFlag10 = false;
                }
                else if (SwordItem10.SwordFlag10 == true)
                {
                    swordManager.AddPlayerSword(10);
                    QuizUnlockManager.Sword10Clear = true;
                    SwordItem10.SwordFlag10 = false;
                }
                else if (ShieldItem10.ShieldFlag10 == true)
                {
                    shieldManager.AddPlayerShield(10);
                    QuizUnlockManager.Shield10Clear = true;
                    ShieldItem10.ShieldFlag10= false;
                }
            }
        }
        else
        {
            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss10 == true)
            {
                
                // アイテム効果
                if (Item10.HPFlag10 == true)
                {
                    hpManager.AddEnemyHP(5);
                    Item10.HPFlag10 = false;
                }
                else if (SwordItem10.SwordFlag10 == true)
                {
                    swordManager.AddEnemySword(5);
                    SwordItem10.SwordFlag10 = false;
                }
                else if (ShieldItem10.ShieldFlag10 == true)
                {
                    shieldManager.AddEnemyShield(5);
                    ShieldItem10.ShieldFlag10 = false;
                }
            }


            if (Score10 == true)
            {
                
                // アイテム効果
                if (Item10.HPFlag10 == true)
                {

                    QuizUnlockManager.Heart10Clear = true;
                    Item10.HPFlag10 = false;
                }
                else if (SwordItem10.SwordFlag10 == true)
                {
                    swordManager.AddEnemySword(5);
                    QuizUnlockManager.Sword10Clear = true;
                    SwordItem10.SwordFlag10 = false;
                }
                else if (ShieldItem10.ShieldFlag10 == true)
                {
                    shieldManager.AddEnemyShield(5);
                    QuizUnlockManager.Shield10Clear = true;
                    ShieldItem10.ShieldFlag10 = false;
                }
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

// 選択肢データ
public class ChoiceData10
{
    public string choiceText;

    public bool isCorrect;
}