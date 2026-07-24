using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class imageQuiz25 : MonoBehaviour
{
    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<ImageQuestionData25> questions;

    private List<ImageQuestionData25> remainingQuestions;

    public Image questionImage;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    public Button[] choiceButtons;

    private ImageQuestionData25 currentQuestion;

    public GameObject panel;

    public HPManager hpManager;

    public ScoreAttackHP attackHP;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    public QuestionCount questionCount;

    public ComboManager comboManager;

    static public bool Score25 = false;

    static public bool Boss25 = false;
    // ✕表示時間
    public float incorrectInterval = 0.5f;

    // 正解表示時間
    public float answerInterval = 2f;

    // シャッフル後の選択肢
    private List<ChoiceData25> shuffledChoices = new List<ChoiceData25>();
    void Start()
    {
        ResetQuestionList();

        answerText.text = "";
        answerText.gameObject.SetActive(false);
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<ImageQuestionData25>(questions);

        // 問題をシャッフル
        for (int i = 0; i < remainingQuestions.Count; i++)
        {
            int randomIndex = Random.Range(i, remainingQuestions.Count);

            ImageQuestionData25 temp = remainingQuestions[i];
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

        Debug.Log("出題: " + currentQuestion.question25);
        Debug.Log("残り問題数: " + remainingQuestions.Count);

        // 問題数を増やす
        questionCount.AddQuestion(1);

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question25;

        questionImage.sprite = currentQuestion.image;
        questionImage.enabled = currentQuestion.image != null;
        // ボタンを有効化
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].interactable = true;
        }
        // 選択肢を作成
        shuffledChoices.Clear();

        for (int i = 0; i < currentQuestion.choices25.Length; i++)
        {
            ChoiceData25 data = new ChoiceData25();

            data.choiceText = currentQuestion.choices25[i];

            data.isCorrect = (i == currentQuestion.correctIndex25);

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
            if (shuffledChoices[i].choiceText.Length >= 8)
            {
                txt.fontSize = 30;   // 6文字以上
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

            ChoiceData25 temp = shuffledChoices[i];
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

            if (Boss25 == true)
            {
                comboManager.AddCombo();
                // アイテム効果
                if (Item25.HPFlag25 == true)
                {
                    hpManager.AddPlayerHP(25);
                    QuizUnlockManager.Heart25Clear = true;
                    Item25.HPFlag25 = false;
                }
                else if (SwordItem25.SwordFlag25 == true)
                {
                    swordManager.AddPlayerSword(25);
                    QuizUnlockManager.Sword25Clear = true;
                    SwordItem25.SwordFlag25 = false;
                }
                else if (ShieldItem25.ShieldFlag25 == true)
                {
                    shieldManager.AddPlayerShield(25);
                    QuizUnlockManager.Shield25Clear = true;
                    ShieldItem25.ShieldFlag25 = false;
                }
            }

            if (Score25 == true)
            {
                comboManager.AddScoreCombo();
                // アイテム効果
                if (Item25.HPFlag25 == true)
                {
                    attackHP.AddPlayerHP(25);
                    QuizUnlockManager.Heart25Clear = true;
                    Item25.HPFlag25 = false;
                }
                else if (SwordItem25.SwordFlag25 == true)
                {
                    swordManager.AddPlayerSword(25);
                    QuizUnlockManager.Sword25Clear = true;
                    SwordItem25.SwordFlag25 = false;
                }
                else if (ShieldItem25.ShieldFlag25 == true)
                {
                    shieldManager.AddPlayerShield(25);
                    QuizUnlockManager.Shield25Clear = true;
                    ShieldItem25.ShieldFlag25 = false;
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
            string correctAnswer = currentQuestion.choices25[currentQuestion.correctIndex25];

            StartCoroutine(ShowCorrectAnswer(playerAnswer, correctAnswer));

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss25 == true)
            {

                // アイテム効果
                if (Item25.HPFlag25 == true)
                {
                    hpManager.AddEnemyHP(13);
                    Item25.HPFlag25 = false;
                }
                else if (SwordItem25.SwordFlag25 == true)
                {
                    swordManager.AddEnemySword(13);
                    SwordItem25.SwordFlag25 = false;
                }
                else if (ShieldItem25.ShieldFlag25 == true)
                {
                    shieldManager.AddEnemyShield(13);
                    ShieldItem25.ShieldFlag25 = false;
                }
            }


            if (Score25 == true)
            {

                // アイテム効果
                if (Item25.HPFlag25 == true)
                {
                    attackHP.SubScore(13);

                    Item25.HPFlag25 = false;
                }
                else if (SwordItem25.SwordFlag25 == true)
                {
                    swordManager.AddEnemySword(13);

                    SwordItem25.SwordFlag25 = false;
                }
                else if (ShieldItem25.ShieldFlag25 == true)
                {
                    shieldManager.AddEnemyShield(13);

                    ShieldItem25.ShieldFlag25 = false;
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
public class ImageQuestionData25
{
    public Sprite image;

    [TextArea]
    public string question25;

    public string[] choices25 = new string[4];

    public int correctIndex25;
}

// 選択肢データ
public class ImageChoiceData25
{
    public string choiceText;

    public bool isCorrect;
}