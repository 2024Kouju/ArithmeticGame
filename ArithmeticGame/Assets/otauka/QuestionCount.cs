using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionCount : MonoBehaviour
{
    public int Questioncount = 0;
 

    public Text QuestionCountText;
 

    void Start()
    {
        UpdateQuestionUI();
    }

    public void AddQuestion(int value)
    {
        Questioncount += value;
        UpdateQuestionUI();
    }


    void UpdateQuestionUI()
    {
        QuestionCountText.text = Questioncount+ "–â–Ú";
        
    }
}
