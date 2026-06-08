using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreResult : MonoBehaviour
{
    public Text hpText;
   

    void Start()
    {
        hpText.text = "スコア : " + ScoreAttackHP.FinalScore+ "点";
        　

   
    }
}
