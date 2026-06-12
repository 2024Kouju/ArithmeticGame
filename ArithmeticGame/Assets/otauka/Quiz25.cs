using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz25 : MonoBehaviour
{
    public AudioSource CirclesoundEffect;

    public AudioSource IncorrectsoundEffect;

    public List<QuestionData25> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData25 currentQuestion;

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
    // 表示までの時間
    public float interval = 0.5f;

    // シャッフル後の選択肢
    private List<ChoiceData25> shuffledChoices = new List<ChoiceData25>();
 
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

        questionText.text = currentQuestion.question25;

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
        if (shuffledChoices[index].isCorrect)
        {
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
                    QuizUnlockManager.Sword5Clear = true;
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
            IncorrectsoundEffect.Play();

            Incorrect.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Boss25 == true)
            {

                // アイテム効果
                if (Item25.HPFlag25 == true)
                {
                    hpManager.AddEnemyHP(25);
                    Item25.HPFlag25 = false;
                }
                else if (SwordItem25.SwordFlag25 == true)
                {
                    swordManager.AddEnemySword(25);
                    SwordItem25.SwordFlag25 = false;
                }
                else if (ShieldItem25.ShieldFlag25 == true)
                {
                    shieldManager.AddEnemyShield(25);
                    ShieldItem25.ShieldFlag25 = false;
                }
            }


            if (Score25 == true)
            {

                // アイテム効果
                if (Item25.HPFlag25 == true)
                {

                    QuizUnlockManager.Heart25Clear = true;
                    Item25.HPFlag25 = false;
                }
                else if (SwordItem25.SwordFlag25 == true)
                {
                    swordManager.AddEnemySword(25);
                    QuizUnlockManager.Sword25Clear = true;
                    SwordItem25.SwordFlag25 = false;
                }
                else if (ShieldItem25.ShieldFlag25 == true)
                {
                    shieldManager.AddEnemyShield(25);
                    QuizUnlockManager.Shield25Clear = true;
                    ShieldItem25.ShieldFlag25 = false;
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
public class QuestionData25
{
    [TextArea]
    public string question25;

    public string[] choices25 = new string[4];

    public int correctIndex25;
}

// 選択肢データ
public class ChoiceData25
{
    public string choiceText;

    public bool isCorrect;
}