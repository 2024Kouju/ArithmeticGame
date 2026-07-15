using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
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

    public GameObject imagequizPanel;


    // 生成開始までの待機時間
    public float startDelay = 3f;

    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(startDelay);

        StartCoroutine(SpawnLoop());
    }
    bool CanSpawn()
    {
        switch (quizType)
        {
            case QuizType.Quiz1:
                return true;

            case QuizType.Quiz5:
                return QuizUnlockManager.Heart1Clear;

            case QuizType.Quiz10:
                return QuizUnlockManager.Heart5Clear;

            case QuizType.Quiz25:
                return QuizUnlockManager.Heart10Clear;

            case QuizType.Quiz50:
                return QuizUnlockManager.Heart25Clear;

            case QuizType.Quiz100:
                return QuizUnlockManager.Heart50Clear;
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
                GameObject.FindGameObjectsWithTag("Hart");

            if (items.Length < maxItems&&CanSpawn())
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

        // QuizタイプごとにPrefab変更
        GameObject prefab = GetPrefab();

        GameObject item =
            Instantiate(prefab, spawnPos, Quaternion.identity);

        ItemMove move = item.GetComponent<ItemMove>();

        move.spawnArea = spawnArea;


        // 中央方向へ移動
        Vector2 center = spawnArea.position;
        Vector2 dir = (center - spawnPos).normalized;

        move.SetDirection(dir);

        // Quiz設定
        switch (quizType)
        {
            case QuizType.Quiz1:
                {
                    Item1 click = item.GetComponent<Item1>();

                    click.panel = panel;
                    click.quizManager = FindObjectOfType<Quiz1>();

                    break;
                }

            case QuizType.Quiz5:
                {
                    Item5 click = item.GetComponent<Item5>();

                    click.quizPanel = panel;
                    click.imageQuizPanel = imagequizPanel;

                    click.quizManager = FindObjectOfType<Quiz5>();
                    click.imageQuizManager = FindObjectOfType<imageQuiz5>();

                    break;
                }
            case QuizType.Quiz10:
                {
                    Item10 click = item.GetComponent<Item10>();

                    click.quizPanel = panel;
                    click.imageQuizPanel = imagequizPanel;

                    click.quizManager = FindObjectOfType<Quiz10>();
                    click.imageQuizManager = FindObjectOfType<imageQuiz10>();

                    break;
                }
            case QuizType.Quiz25:
                {
                    Item25 click = item.GetComponent<Item25>();

                    click.quizPanel = panel;
                    click.imageQuizPanel = imagequizPanel;

                    click.quizManager = FindObjectOfType<Quiz25>();
                    click.imageQuizManager = FindObjectOfType<imageQuiz25>();

                    break;
                }
            case QuizType.Quiz50:
                {
                    Item50 click = item.GetComponent<Item50>();

                    click.quizPanel = panel;
                    click.imageQuizPanel = imagequizPanel;

                    click.quizManager = FindObjectOfType<Quiz50>();
                    click.imageQuizManager = FindObjectOfType<imageQuiz50>();

                    break;
                }
            case QuizType.Quiz100:
                {
                    Item100 click = item.GetComponent<Item100>();

                    click.quizPanel = panel;
                    click.imageQuizPanel = imagequizPanel;

                    click.quizManager = FindObjectOfType<Quiz100>();
                    click.imageQuizManager = FindObjectOfType<imageQuiz100>();

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