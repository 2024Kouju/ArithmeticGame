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
    public TextMeshProUGUI answerText;

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
    // ✕を表示する時間
    public float incorrectInterval = 0.5f;

    // 正解を表示する時間
    public float answerInterval = 2f;

    // シャッフル後の選択肢
    private List<ChoiceData100> shuffledChoices = new List<ChoiceData100>();

    void Start()
    {
        ResetQuestionList();

        answerText.text = "";
        answerText.gameObject.SetActive(false);
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<QuestionData100>(questions);

        // 問題をシャッフル
        for (int i = 0; i < remainingQuestions.Count; i++)
        {
            int randomIndex = Random.Range(i, remainingQuestions.Count);

            QuestionData100 temp = remainingQuestions[i];
            remainingQuestions[i] = remainingQuestions[randomIndex];
            remainingQuestions[randomIndex] = temp;
        }

        Debug.Log("問題をシャッフルしました");
    }
    public void ShowRandomQuestion()
    {
        answerText.text = "";
        answerText.gameObject.SetActive(false);

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

        // シャッフル済みリストの先頭から出題
        currentQuestion = remainingQuestions[0];

        // 出題済みなので削除
        remainingQuestions.RemoveAt(0);

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
        foreach (Button button in choiceButtons)
        {
            button.interactable = true;
        }
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

            // 文字数によって文字サイズを変更
            if (shuffledChoices[i].choiceText.Length >= 7)
            {
                txt.fontSize = 25;   // 6文字以上
            }
            else
            {
                txt.fontSize = 35;   // 通常サイズ
            }

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
        foreach (Button button in choiceButtons)
        {
            button.interactable = false;
        }


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
            // 正解を取得
            string playerAnswer = shuffledChoices[index].choiceText;
            string correctAnswer = currentQuestion.choices100[currentQuestion.correctIndex100];

            StartCoroutine(ShowCorrectAnswer(playerAnswer, correctAnswer));

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
        yield return new WaitForSeconds(incorrectInterval);

        Circle.SetActive(false);

        Incorrect.SetActive(false);

        answerText.gameObject.SetActive(false);

        panel.SetActive(false);
    }
    IEnumerator ShowCorrectAnswer(string playerAnswer, string correctAnswer)
    {
        // ① ✕を表示
        yield return new WaitForSeconds(incorrectInterval);

        // ② ✕と問題・選択肢を消す
        Incorrect.SetActive(false);

        questionText.gameObject.SetActive(false);

        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(false);
        }
        // ③ 正解を表示
        answerText.text =
            "あなたの回答\n" +
            "「" + playerAnswer + "」\n" +
            "正解は\n" +
            "「" + correctAnswer + "」";
        answerText.gameObject.SetActive(true);

        // ④ 正解を表示
        yield return new WaitForSeconds(answerInterval);

        // ⑤ 正解を消す
        answerText.gameObject.SetActive(false);

        // ⑥ 問題UIを戻す
        questionText.gameObject.SetActive(true);

        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(true);
        }

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