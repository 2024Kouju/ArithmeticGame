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

    private List<QuestionData10> remainingQuestions;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
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
    // ✕を表示する時間
    public float incorrectInterval = 0.5f;

    // 正解を表示する時間
    public float answerInterval = 2f;

    // シャッフル後の選択肢
    private List<ChoiceData10> shuffledChoices = new List<ChoiceData10>();

    void Start()
    {
        ResetQuestionList();

        answerText.text = "";
        answerText.gameObject.SetActive(false);
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<QuestionData10>(questions);

        // 問題をシャッフル
        for (int i = 0; i < remainingQuestions.Count; i++)
        {
            int randomIndex = Random.Range(i, remainingQuestions.Count);

            QuestionData10 temp = remainingQuestions[i];
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

        Debug.Log("出題: " + currentQuestion.question10);
        Debug.Log("残り問題数: " + remainingQuestions.Count);

        // 問題数を増やす
        questionCount.AddQuestion(1);

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question10;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].interactable = true;
        }
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

            ChoiceData10 temp = shuffledChoices[i];
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
            Rightorwrong.Wrong++;
            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            // 正解を取得
            string playerAnswer = shuffledChoices[index].choiceText;
            string correctAnswer = currentQuestion.choices10[currentQuestion.correctIndex10];

            StartCoroutine(ShowCorrectAnswer(playerAnswer, correctAnswer));

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
                    attackHP.SubScore(10);

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
        // ✕を表示
        yield return new WaitForSeconds(incorrectInterval);

        // ✕と問題・選択肢を消す
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

        // 正解表示時間
        yield return new WaitForSeconds(answerInterval);

        // 答えを消す
        answerText.gameObject.SetActive(false);

        // 問題UIを戻す
        questionText.gameObject.SetActive(true);

        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(true);
        }

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