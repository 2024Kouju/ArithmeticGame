using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwordManager : MonoBehaviour
{
    public static int FinalPlayerSword;
    public static int FinalEnemySword;

    public int playerSword = 100;
    public int enemySword = 100;

    public Text playerSwordText;
    public Text enemySwordText;
    public Text playerSwordStatusText;
    public Text enemySwordStatusText;

    // ìGÇÃçUåÇóÕÉAÉbÉvï\é¶ópÉLÉÖÅ[
    private Queue<int> enemySwordQueue = new Queue<int>();
    private bool isShowingEnemySword = false;

    void Start()
    {
        UpdateSwordUI();

        playerSwordStatusText.gameObject.SetActive(false);
        enemySwordStatusText.gameObject.SetActive(false);

        InvokeRepeating(nameof(IncreaseEnemySword), 21f, 15f);
    }

    void ShowPlayerSwordStatus(int value)
    {
        playerSwordStatusText.gameObject.SetActive(true);
        playerSwordStatusText.text = "çUåÇóÕ +" + value;

        CancelInvoke(nameof(HidePlayerSwordStatus));
        Invoke(nameof(HidePlayerSwordStatus), 1f);
    }

    void HidePlayerSwordStatus()
    {
        playerSwordStatusText.gameObject.SetActive(false);
    }

    void ShowEnemySwordStatus(int value)
    {
        enemySwordQueue.Enqueue(value);

        if (!isShowingEnemySword)
        {
            StartCoroutine(ShowEnemySwordQueue());
        }
    }

    IEnumerator ShowEnemySwordQueue()
    {
        isShowingEnemySword = true;

        while (enemySwordQueue.Count > 0)
        {
            int value = enemySwordQueue.Dequeue();

            enemySwordStatusText.gameObject.SetActive(true);
            enemySwordStatusText.text = "çUåÇóÕ +" + value;

            yield return new WaitForSeconds(1f);

            enemySwordStatusText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

        isShowingEnemySword = false;
    }

    void HideEnemySwordStatus()
    {
        enemySwordStatusText.gameObject.SetActive(false);
    }

    public void AddPlayerSword(int value)
    {
        playerSword += value;

        if (value > 0)
        {
            ShowPlayerSwordStatus(value);
        }

        UpdateSwordUI();
    }

    public void AddEnemySword(int value)
    {
        enemySword += value;

        if (value > 0)
        {
            ShowEnemySwordStatus(value);
        }

        UpdateSwordUI();
    }

    void IncreaseEnemySword()
    {
        enemySword += 2;

        ShowEnemySwordStatus(5);

        UpdateSwordUI();
    }

    public void SaveFinalStatus()
    {
        FinalPlayerSword = playerSword;
        FinalEnemySword = enemySword;
    }

    void UpdateSwordUI()
    {
        playerSwordText.text = "çUåÇóÕ : " + playerSword;
        enemySwordText.text = "çUåÇóÕ : " + enemySword;
    }
}