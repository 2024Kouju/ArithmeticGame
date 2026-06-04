using System.Collections;
using UnityEngine;

public class SwordItemController : MonoBehaviour
{
    public enum QuizType
    {
        Quiz1,
        Quiz5,
        Quiz10,
        Quiz25,
        Quiz50,
        Quiz100
    }

    public QuizType quizType;

    [Header("Prefab")]
    public GameObject item1Prefab;
    public GameObject item5Prefab;
    public GameObject item10Prefab;
    public GameObject item25Prefab;
    public GameObject item50Prefab;
    public GameObject item100Prefab;

    public float minTime = 1f;
    public float maxTime = 3f;

    public RectTransform spawnArea;


    public int maxItems = 10;

    public GameObject panel;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    bool CanSpawn()
    {
        switch (quizType)
        {
            case QuizType.Quiz1:
                return true;

            case QuizType.Quiz5:
                return QuizUnlockManager.Sword1Clear;

            case QuizType.Quiz10:
                return QuizUnlockManager.Sword5Clear;

            case QuizType.Quiz25:
                return QuizUnlockManager.Sword10Clear;

            case QuizType.Quiz50:
                return QuizUnlockManager.Sword25Clear;

            case QuizType.Quiz100:
                return QuizUnlockManager.Sword50Clear;
        }

        return false;
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(waitTime);

            GameObject[] items =
                GameObject.FindGameObjectsWithTag("Sword");

            if (items.Length < maxItems && CanSpawn())
            {
                SpawnItem();
            }
        }
    }

    void SpawnItem()
    {
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        float x, y;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                x = Random.Range(bottomLeft.x, topRight.x);
                y = topRight.y + 1f;
                break;

            case 1:
                x = Random.Range(bottomLeft.x, topRight.x);
                y = bottomLeft.y - 1f;
                break;

            case 2:
                x = bottomLeft.x - 1f;
                y = Random.Range(bottomLeft.y, topRight.y);
                break;

            default:
                x = topRight.x + 1f;
                y = Random.Range(bottomLeft.y, topRight.y);
                break;
        }

        Vector2 spawnPos = new Vector2(x, y);

        GameObject prefab = GetPrefab();

        GameObject item =
            Instantiate(prefab, spawnPos, Quaternion.identity);

        ItemMove move = item.GetComponent<ItemMove>();

        move.spawnArea = spawnArea;
  

        move.SetRandomDirection();

        switch (quizType)
        {
            case QuizType.Quiz1:
                {
                    SwordItem1 click = item.GetComponent<SwordItem1>();
                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz1>();
                    break;
                }

            case QuizType.Quiz5:
                {
                    SwordItem5 click = item.GetComponent<SwordItem5>();
                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz5>();
                    break;
                }

            case QuizType.Quiz10:
                {
                    SwordItem10 click = item.GetComponent<SwordItem10>();
                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz10>();
                    break;
                }

            case QuizType.Quiz25:
                {
                    SwordItem25 click = item.GetComponent<SwordItem25>();
                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz25>();
                    break;
                }

            case QuizType.Quiz50:
                {
                    SwordItem50 click = item.GetComponent<SwordItem50>();
                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz50>();
                    break;
                }

            case QuizType.Quiz100:
                {
                    SwordItem100 click = item.GetComponent<SwordItem100>();
                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz100>();
                    break;
                }
        }
    }

    GameObject GetPrefab()
    {
        switch (quizType)
        {
            case QuizType.Quiz1:
                return item1Prefab;

            case QuizType.Quiz5:
                return item5Prefab;

            case QuizType.Quiz10:
                return item10Prefab;

            case QuizType.Quiz25:
                return item25Prefab;

            case QuizType.Quiz50:
                return item50Prefab;

            case QuizType.Quiz100:
                return item100Prefab;
        }

        return item1Prefab;
    }
}