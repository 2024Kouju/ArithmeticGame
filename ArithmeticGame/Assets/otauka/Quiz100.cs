using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Quiz100 : MonoBehaviour
{

    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<QuestionData100> questions;

    private List<QuestionData100> remainingQuestions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData100 currentQuestion;

    public GameObject panel;

    public HPManager hpManager;

    public ScoreAttackHP attackHP;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    public QuestionCount questionCount;

    public ComboManager comboManager;


    static public bool Score100 = false;

    static public bool Boss100 = false;
    // 表示までの時間
    public float interval = 0.5f;

    // シャッフル後の選択肢
    private List<ChoiceData100> shuffledChoices = new List<ChoiceData100>();

    void Start()
    {
        ResetQuestionList();
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<QuestionData100>(questions);
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

        Debug.Log("出題: " + currentQuestion.question100);
        Debug.Log("残り問題数: " + remainingQuestions.Count);

        // 問題数を増やす
        questionCount.AddQuestion(1);

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question100;

        // 選択肢を作成
        shuffledChoices.Clear();

        for (int i = 0; i < currentQuestion.choices100.Length; i++)
        {
            ChoiceData100 data = new ChoiceData100();

            data.choiceText = currentQuestion.choices100[i];

            data.isCorrect = (i == currentQuestion.correctIndex100);

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

            ChoiceData100 temp = shuffledChoices[i];
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

            if (Boss100 == true)
            {
                comboManager.AddCombo();
                // アイテム効果
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

            if (Score100 == true)
            {
                comboManager.AddScoreCombo();
                // アイテム効果
                if (Item100.HPFlag100 == true)
                {
                    attackHP.AddPlayerHP(100);
                    
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
        }
        else
        {
            Rightorwrong.Wrong++;

            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss100 == true)
            {

                // アイテム効果
                if (Item100.HPFlag100 == true)
                {
                    hpManager.AddEnemyHP(50);
                    Item100.HPFlag100 = false;
                }
                else if (SwordItem100.SwordFlag100 == true)
                {
                    swordManager.AddEnemySword(50);
                    SwordItem100.SwordFlag100 = false;
                }
                else if (ShieldItem100.ShieldFlag100 == true)
                {
                    shieldManager.AddEnemyShield(50);
                    ShieldItem100.ShieldFlag100 = false;
                }
            }


            if (Score100 == true)
            {

                // アイテム効果
                if (Item100.HPFlag100 == true)
                {
                    attackHP.SubScore(100);
                    Item100.HPFlag100 = false;
                }
                else if (SwordItem100.SwordFlag100 == true)
                {
                    swordManager.AddEnemySword(50);
                  
                    SwordItem100.SwordFlag100 = false;
                }
                else if (ShieldItem100.ShieldFlag100 == true)
                {
                    shieldManager.AddEnemyShield(50);
                    
                    ShieldItem100.ShieldFlag100 = false;
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
public class QuestionData100
{
    [TextArea]
    public string question100;

    public string[] choices100 = new string[4];

    public int correctIndex100;
}

// 選択肢データ
public class ChoiceData100
{
    public string choiceText;

    public bool isCorrect;
}