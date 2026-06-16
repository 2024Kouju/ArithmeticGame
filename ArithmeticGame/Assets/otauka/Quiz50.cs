using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz50 : MonoBehaviour
{
    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<QuestionData50> questions;

    private List<QuestionData50> remainingQuestions;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;

    public Button[] choiceButtons;

    private QuestionData50 currentQuestion;

    [SerializeField]
    private int questionIndex = 0;


    public GameObject panel;



    public HPManager hpManager;

    public ScoreAttackHP attackHP;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public QuestionCount questionCount;

    public ComboManager comboManager;
 

    public GameObject Incorrect;

    static public bool Score50 = false;

    static public bool Boss50 = false;

    public float normalFontSize = 36f;
    public float smallFontSize = 24f;

    // ✕表示時間
    public float incorrectInterval = 0.5f;

    // 正解表示時間
    public float answerInterval = 2f;

    // シャッフル後の選択肢
    private List<ChoiceData50> shuffledChoices = new List<ChoiceData50>();

    void Start()
    {
        ResetQuestionList();

        answerText.text = "";
        answerText.gameObject.SetActive(false);
    }
    void ResetQuestionList()
    {
        remainingQuestions = new List<QuestionData50>(questions);
    }
    public void ShowRandomQuestion()
    {
        //if (questionIndex < 0 || questionIndex >= questions.Count)
        //{
        //    Debug.LogError("問題番号が範囲外です");
        //    return;
        //}

        //currentQuestion = questions[questionIndex];

        //questionText.text = currentQuestion.question50;

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

       // ランダム選択
        int randomIndex = Random.Range(0, remainingQuestions.Count);

        currentQuestion = remainingQuestions[randomIndex];

        //出題済み問題を除外
        remainingQuestions.RemoveAt(randomIndex);

        Debug.Log("出題: " + currentQuestion.question50);
        Debug.Log("残り問題数: " + remainingQuestions.Count);

        // 問題数を増やす
        questionCount.AddQuestion(1);

        if (questionText == null)
        {
            Debug.LogError("questionText未設定");
            return;
        }

        questionText.text = currentQuestion.question50;

        // ボタンを再び押せるようにする
        foreach (Button button in choiceButtons)
        {
            button.interactable = true;
        }
        // 23文字以上なら文字サイズを変更
        if (currentQuestion.question50.Length >= 23)
        {
            questionText.fontSize = smallFontSize;
        }
        else
        {
            questionText.fontSize = normalFontSize;
        }

        // 選択肢を作成
        shuffledChoices.Clear();

        for (int i = 0; i < currentQuestion.choices50.Length; i++)
        {
            ChoiceData50 data = new ChoiceData50();

            data.choiceText = currentQuestion.choices50[i];

            data.isCorrect = (i == currentQuestion.correctIndex50);

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

            ChoiceData50 temp = shuffledChoices[i];
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

            if (Boss50 == true)
            {
                comboManager.AddCombo();
                // アイテム効果
                if (Item50.HPFlag50 == true)
                {
                    hpManager.AddPlayerHP(50);
                    QuizUnlockManager.Heart50Clear = true;
                    Item50.HPFlag50 = false;
                }
                else if (SwordItem50.SwordFlag50 == true)
                {
                    swordManager.AddPlayerSword(50);
                    QuizUnlockManager.Sword50Clear = true;
                    SwordItem50.SwordFlag50 = false;
                }
                else if (ShieldItem50.ShieldFlag50 == true)
                {
                    shieldManager.AddPlayerShield(50);
                    QuizUnlockManager.Shield50Clear = true;
                    ShieldItem50.ShieldFlag50 = false;
                }
            }

            if (Score50 == true)
            {
                comboManager.AddScoreCombo();
                // アイテム効果
                if (Item50.HPFlag50 == true)
                {
                    attackHP.AddPlayerHP(50);
                    QuizUnlockManager.Heart50Clear = true;
                    Item50.HPFlag50 = false;
                }
                else if (SwordItem50.SwordFlag50 == true)
                {
                    swordManager.AddPlayerSword(50);
                    QuizUnlockManager.Sword50Clear = true;
                    SwordItem50.SwordFlag50 = false;
                }
                else if (ShieldItem50.ShieldFlag50 == true)
                {
                    shieldManager.AddPlayerShield(50);
                    QuizUnlockManager.Shield50Clear = true;
                    ShieldItem50.ShieldFlag50 = false;
                }
            }
        }
        else
        {
            Rightorwrong.Wrong++;

            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            // 正解を取得
            string correctAnswer =
                currentQuestion.choices50[currentQuestion.correctIndex50];

            StartCoroutine(ShowCorrectAnswer(correctAnswer));

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss50 == true)
            {

                // アイテム効果
                if (Item50.HPFlag50 == true)
                {
                    hpManager.AddEnemyHP(25);
                    Item50.HPFlag50 = false;
                }
                else if (SwordItem50.SwordFlag50 == true)
                {
                    swordManager.AddEnemySword(25);
                    SwordItem50.SwordFlag50 = false;
                }
                else if (ShieldItem50.ShieldFlag50 == true)
                {
                    shieldManager.AddEnemyShield(25);
                    ShieldItem50.ShieldFlag50 = false;
                }
            }


            if (Score50 == true)
            {

                // アイテム効果
                if (Item50.HPFlag50 == true)
                {
                    attackHP.SubScore(50);
                    Item50.HPFlag50 = false;
                }
                else if (SwordItem50.SwordFlag50 == true)
                {
                    swordManager.AddEnemySword(25);
                 
                    SwordItem50.SwordFlag50 = false;
                }
                else if (ShieldItem50.ShieldFlag50 == true)
                {
                    shieldManager.AddEnemyShield(25);
               
                    ShieldItem50.ShieldFlag50 = false;
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

    IEnumerator ShowCorrectAnswer(string correctAnswer)
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
        answerText.text = "正解は\n「" + correctAnswer + "」です";
        answerText.gameObject.SetActive(true);

        answerText.gameObject.SetActive(true);

        // 正解を表示
        yield return new WaitForSeconds(answerInterval);

        // 正解を消す
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
public class QuestionData50
{
    [TextArea]
    public string question50;

    public string[] choices50 = new string[4];

    public int correctIndex50;
}

// 選択肢データ
public class ChoiceData50
{
    public string choiceText;

    public bool isCorrect;
}