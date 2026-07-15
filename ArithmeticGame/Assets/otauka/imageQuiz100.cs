using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class imageQuiz100 : MonoBehaviour
{
    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<ImageQuestionData100> questions;

    private List<ImageQuestionData100> remainingQuestions;

    public Image questionImage;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    public Button[] choiceButtons;

    private ImageQuestionData100 currentQuestion;

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
    // ✕表示時間
    public float incorrectInterval = 0.5f;

    // 正解表示時間
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
        remainingQuestions = new List<ImageQuestionData100>(questions);

        // 問題をシャッフル
        for (int i = 0; i < remainingQuestions.Count; i++)
        {
            int randomIndex = Random.Range(i, remainingQuestions.Count);

            ImageQuestionData100 temp = remainingQuestions[i];
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

        questionImage.sprite = currentQuestion.image;
        questionImage.enabled = currentQuestion.image != null;
        // ボタンを有効化
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].interactable = true;
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
        // 二重押し防止
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

            if (Score100 == true)
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


            // 正解を取得
            string playerAnswer = shuffledChoices[index].choiceText;
            string correctAnswer = currentQuestion.choices100[currentQuestion.correctIndex100];

            StartCoroutine(ShowCorrectAnswer(playerAnswer, correctAnswer));

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss100 == true)
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


            if (Score100== true)
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
        questionImage.gameObject.SetActive(false);

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

        // 正解表示
        yield return new WaitForSeconds(answerInterval);

        // 答えを消す
        answerText.gameObject.SetActive(false);

        questionText.gameObject.SetActive(true);
        questionImage.gameObject.SetActive(true);

        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(true);
        }

        panel.SetActive(false);
    }
}

[System.Serializable]
public class ImageQuestionData100
{
    public Sprite image;

    [TextArea]
    public string question100;

    public string[] choices100 = new string[4];

    public int correctIndex100;
}

// 選択肢データ
public class ImageChoiceData100
{
    public string choiceText;

    public bool isCorrect;
}