using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Quiz5 : MonoBehaviour
{
    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<QuestionData5> questions;

    private List<QuestionData5> remainingQuestions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData5 currentQuestion;

    public GameObject panel;

    public HPManager hpManager;

    public ScoreAttackHP attackHP;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    public QuestionCount questionCount;

    public ComboManager comboManager;

    static public bool Score5 = false;

    static public bool Boss5 = false;
    // 表示までの時間
    public float interval = 0.5f;

    // シャッフル後の選択肢
    private List<ChoiceData5> shuffledChoices = new List<ChoiceData5>();
    void Start()
    {
        ResetQuestionList();
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<QuestionData5>(questions);
    }
    public void ShowRandomQuestion()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("questionsが空");
            return;
        }

        // 全問出題したらリセット
        if (remainingQuestions.Count == 0)
        {
            Debug.Log("全問出題したのでリセット");
            ResetQuestionList();
        }

        // ランダム選択
        int randomIndex = Random.Range(0, remainingQuestions.Count);

        currentQuestion = remainingQuestions[randomIndex];

        // 出題済み問題を除外
        remainingQuestions.RemoveAt(randomIndex);

        Debug.Log("出題: " + currentQuestion.question5);
        Debug.Log("残り問題数: " + remainingQuestions.Count);

        // 問題数を増やす
        questionCount.AddQuestion(1);

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question5;

        // 選択肢を作成
        shuffledChoices.Clear();

        for (int i = 0; i < currentQuestion.choices5.Length; i++)
        {
            ChoiceData5 data = new ChoiceData5();

            data.choiceText = currentQuestion.choices5[i];

            data.isCorrect = (i == currentQuestion.correctIndex5);

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

            ChoiceData5 temp = shuffledChoices[i];
            shuffledChoices[i] = shuffledChoices[randomIndex];
            shuffledChoices[randomIndex] = temp;
        }
    }

    void CheckAnswer(int index)
    {
        if (shuffledChoices[index].isCorrect)
        {
            Rightorwrong.Right++;

            CirclesoundEffect.Play();

            Circle.SetActive(true);

            StartCoroutine(OpenPanel());

           

            Debug.Log("正解！");

            if (Boss5 == true)
            {
                comboManager.AddCombo();
                // アイテム効果
                if (Item5.HPFlag5 == true)
                {
                    hpManager.AddPlayerHP(5);
                    QuizUnlockManager.Heart5Clear = true;
                    Item5.HPFlag5 = false;
                }
                else if (SwordItem5.SwordFlag5 == true)
                {
                    swordManager.AddPlayerSword(5);
                    QuizUnlockManager.Sword5Clear = true;
                    SwordItem5.SwordFlag5 = false;
                }
                else if (ShieldItem5.ShieldFlag5 == true)
                {
                    shieldManager.AddPlayerShield(5);
                    QuizUnlockManager.Shield5Clear = true;
                    ShieldItem5.ShieldFlag5 = false;
                }
            }

            if (Score5 == true)
            {
                comboManager.AddScoreCombo();
                // アイテム効果
                if (Item5.HPFlag5 == true)
                {
                    attackHP.AddPlayerHP(5);
                    QuizUnlockManager.Heart5Clear = true;
                    Item5.HPFlag5 = false;
                }
                else if (SwordItem5.SwordFlag5 == true)
                {
                    swordManager.AddPlayerSword(5);
                    QuizUnlockManager.Sword5Clear = true;
                    SwordItem5.SwordFlag5 = false;
                }
                else if (ShieldItem5.ShieldFlag5 == true)
                {
                    shieldManager.AddPlayerShield(5);
                    QuizUnlockManager.Shield5Clear = true;
                    ShieldItem5.ShieldFlag5 = false;
                }
            }
        }
        else
        {
            Rightorwrong.Wrong++;

            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss5 == true)
            {
                
                // アイテム効果
                if (Item5.HPFlag5 == true)
                {
                    hpManager.AddEnemyHP(3);
                    Item5.HPFlag5 = false;
                }
                else if (SwordItem5.SwordFlag5 == true)
                {
                    swordManager.AddEnemySword(3);
                    SwordItem5.SwordFlag5 = false;
                }
                else if (ShieldItem5.ShieldFlag5 == true)
                {
                    shieldManager.AddEnemyShield(3);
                    ShieldItem5.ShieldFlag5 = false;
                }
            }


            if (Score5 == true)
            {
                
                // アイテム効果
                if (Item5.HPFlag5 == true)
                {
                    attackHP.SubScore(5);

                    Item5.HPFlag5 = false;
                }
                else if (SwordItem5.SwordFlag5 == true)
                {
                    swordManager.AddEnemySword(3);
     
                    SwordItem5.SwordFlag5 = false;
                }
                else if (ShieldItem5.ShieldFlag5 == true)
                {
                    shieldManager.AddEnemyShield(3);
                
                    ShieldItem5.ShieldFlag5 = false;
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
public class QuestionData5
{
    [TextArea]
    public string question5;

    public string[] choices5 = new string[4];

    public int correctIndex5;
}

// 選択肢データ
public class ChoiceData5
{
    public string choiceText;

    public bool isCorrect;
}