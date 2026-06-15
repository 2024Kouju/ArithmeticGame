using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz1 : MonoBehaviour
{
    

    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<QuestionData1> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData1 currentQuestion;

    private List<QuestionData1> remainingQuestions;

    public GameObject panel;

    public HPManager hpManager;

    public ScoreAttackHP attackHP;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    public QuestionCount questionCount;

    public ComboManager comboManager;


    // 表示までの時間
    public float interval = 0.5f;

    static public bool Score = false;

    static public bool Boss = false;

    // シャッフル後の選択肢
    private List<ChoiceData> shuffledChoices = new List<ChoiceData>();


    void Start()
    {
        ResetQuestionList();
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<QuestionData1>(questions);
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

        // 出題済みリストから削除
        remainingQuestions.RemoveAt(randomIndex);

        Debug.Log("出題: " + currentQuestion.question1);
        Debug.Log("残り問題数: " + remainingQuestions.Count);

        // 問題数を増やす
        questionCount.AddQuestion(1);

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question1;

        // 選択肢を作成
        shuffledChoices.Clear();

        for (int i = 0; i < currentQuestion.choices1.Length; i++)
        {
            ChoiceData data = new ChoiceData();

            data.choiceText = currentQuestion.choices1[i];
            data.isCorrect = (i == currentQuestion.correctIndex1);

            shuffledChoices.Add(data);
        }

        // 選択肢をシャッフル
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

            ChoiceData temp = shuffledChoices[i];
            shuffledChoices[i] = shuffledChoices[randomIndex];
            shuffledChoices[randomIndex] = temp;
        }
    }

    void CheckAnswer(int index)
    {
        // 正解
        if (shuffledChoices[index].isCorrect)
        {
            Rightorwrong.Right++;

            CirclesoundEffect.Play();

            Circle.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("正解！");



            if (Boss == true)
            {
                comboManager.AddCombo();
                // アイテム効果
                if (Item1.HPFlag1 == true)
                {
                    hpManager.AddPlayerHP(1);
                    QuizUnlockManager.Heart1Clear = true;
                    Item1.HPFlag1 = false;
                }
                else if (SwordItem1.SwordFlag1 == true)
                {
                    swordManager.AddPlayerSword(1);
                    QuizUnlockManager.Sword1Clear = true;
                    SwordItem1.SwordFlag1 = false;
                }
                else if (ShieldItem1.ShieldFlag1 == true)
                {
                    shieldManager.AddPlayerShield(1);
                    QuizUnlockManager.Shield1Clear = true;
                    ShieldItem1.ShieldFlag1 = false;
                }
            }

            if (Score == true)
            {
                 comboManager.AddScoreCombo();
                // アイテム効果
                if (Item1.HPFlag1 == true)
                {
                    attackHP.AddPlayerHP(1);
                    QuizUnlockManager.Heart1Clear = true;
                    Item1.HPFlag1 = false;
                }
                else if (SwordItem1.SwordFlag1 == true)
                {
                    swordManager.AddPlayerSword(1);
                    QuizUnlockManager.Sword1Clear = true;
                    SwordItem1.SwordFlag1 = false;
                }
                else if (ShieldItem1.ShieldFlag1 == true)
                {
                    shieldManager.AddPlayerShield(1);
                    QuizUnlockManager.Shield1Clear = true;
                    ShieldItem1.ShieldFlag1 = false;
                }
            }
 
        }
        // 不正解
        else
        {
            Rightorwrong.Wrong++;
            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss == true)
            {
                // アイテム効果
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
           

            if (Score == true)
            {
                
                // アイテム効果
                if (Item1.HPFlag1 == true)
                {
                    attackHP.SubScore(1);
            
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

// 選択肢データ
public class ChoiceData
{
    public string choiceText;

    public bool isCorrect;
}