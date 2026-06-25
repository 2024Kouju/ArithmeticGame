using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldManager : MonoBehaviour
{
    public static int FinalPlayerShield;
    public static int FinalEnemyShield;

    public int playerShield = 100;
    public int enemyShield = 100;

    public Text playerShieldText;
    public Text enemyShieldText;
    public Text playerShieldStatusText;
    public Text enemyShieldStatusText;

    // ìGÇÃñhå‰óÕÉAÉbÉvï\é¶ópÉLÉÖÅ[
    private Queue<int> enemyShieldQueue = new Queue<int>();
    private bool isShowingEnemyShield = false;

    void Start()
    {
        UpdateShieldUI();

        playerShieldStatusText.gameObject.SetActive(false);
        enemyShieldStatusText.gameObject.SetActive(false);

        InvokeRepeating(nameof(IncreaseEnemyShield), 21f, 15f);
    }

    void ShowPlayerShieldStatus(int value)
    {
        playerShieldStatusText.gameObject.SetActive(true);
        playerShieldStatusText.text = "ñhå‰óÕ +" + value;

        CancelInvoke(nameof(HidePlayerShieldStatus));
        Invoke(nameof(HidePlayerShieldStatus), 1f);
    }

    void HidePlayerShieldStatus()
    {
        playerShieldStatusText.gameObject.SetActive(false);
    }

    void ShowEnemyShieldStatus(int value)
    {
        enemyShieldQueue.Enqueue(value);

        if (!isShowingEnemyShield)
        {
            StartCoroutine(ShowEnemyShieldQueue());
        }
    }

    IEnumerator ShowEnemyShieldQueue()
    {
        isShowingEnemyShield = true;

        while (enemyShieldQueue.Count > 0)
        {
            int value = enemyShieldQueue.Dequeue();

            enemyShieldStatusText.gameObject.SetActive(true);
            enemyShieldStatusText.text = "ñhå‰óÕ +" + value;

            yield return new WaitForSeconds(1f);

            enemyShieldStatusText.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.1f);
        }

        isShowingEnemyShield = false;
    }

    void HideEnemyShieldStatus()
    {
        enemyShieldStatusText.gameObject.SetActive(false);
    }

    public void AddPlayerShield(int value)
    {
        playerShield += value;

        if (value > 0)
        {
            ShowPlayerShieldStatus(value);
        }

        UpdateShieldUI();
    }

    public void AddEnemyShield(int value)
    {
        enemyShield += value;

        if (value > 0)
        {
            ShowEnemyShieldStatus(value);
        }

        UpdateShieldUI();
    }

    void IncreaseEnemyShield()
    {
        enemyShield += 5;

        ShowEnemyShieldStatus(5);

        UpdateShieldUI();
    }

    public void SaveFinalStatus()
    {
        FinalPlayerShield = playerShield;
        FinalEnemyShield = enemyShield;
    }

    void UpdateShieldUI()
    {
        if (playerShieldText != null)
            playerShieldText.text = "ñhå‰óÕ : " + playerShield;

        if (enemyShieldText != null)
            enemyShieldText.text = "ñhå‰óÕ : " + enemyShield;
    }
}