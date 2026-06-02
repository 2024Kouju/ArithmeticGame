using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz50 : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip CirclesoundEffect;

    public AudioClip IncorrectsoundEffect;

    public List<QuestionData50> questions;

    public TextMeshProUGUI questionText;
    public Button[] choiceButtons;

    private QuestionData50 currentQuestion;

    public GameObject panel;

    public HPManager hpManager;

    public SwordManager swordManager;

    public ShieldManager shieldManager;

    public GameObject Circle;

    public GameObject Incorrect;

    public ComboManager comboManager;

    public QuestionCount questionCount;

    // 表示までの時間
    public float interval = 0.5f;

    // シャッフル後の選択肢
    private List<ChoiceData50> shuffledChoices = new List<ChoiceData50>();
    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }
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

        questionText.text = currentQuestion.question50;

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
        if (shuffledChoices[index].isCorrect)
        {
            Circle.SetActive(true);

            StartCoroutine(OpenPanel());

            comboManager.AddCombo();

            Debug.Log("正解！");

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
        else
        {
            Incorrect.SetActive(true);

            StartCoroutine(OpenPanel());

            Debug.Log("不正解！");

            comboManager.ResetCombo();

            if (Item50.HPFlag50 == true)
            {
                hpManager.AddEnemyHP(50);
                Item50.HPFlag50 = false;
            }
            else if (SwordItem50.SwordFlag50 == true)
            {
                swordManager.AddEnemySword(50);
                SwordItem50.SwordFlag50 = false;
            }
            else if (ShieldItem50.ShieldFlag50 == true)
            {
                shieldManager.AddEnemyShield(50);
                ShieldItem50.ShieldFlag50 = false;
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