using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreAttackHP : MonoBehaviour
{
    public int playerHP = 100;


    public Text playerHPText;
 

    void Start()
    {
        UpdateHPUI();

   
    }

    public void AddPlayerHP(int value)
    {
        playerHP += value;

        UpdateHPUI();

        if (playerHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }



  

    void UpdateHPUI()
    {
        playerHPText.text = "HP : " + playerHP;
       
    }
}