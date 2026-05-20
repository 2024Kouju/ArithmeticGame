using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItemController5 : MonoBehaviour
{
    public GameObject itemPrefab;
    public float minTime = 1f;
    public float maxTime = 3f;

    public RectTransform spawnArea;
    public RectTransform allowArea;
    public RectTransform denyArea;

    public GameObject panel;

    public int maxItems = 10;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(waitTime);

            // Itemƒ^ƒO‚Ì”‚ğæ“¾
            GameObject[] items =
                GameObject.FindGameObjectsWithTag("Item");

            // ãŒÀ–¢–‚È‚ç¶¬
            if (items.Length < maxItems)
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

        GameObject item =
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        ItemMove move = item.GetComponent<ItemMove>();

        move.spawnArea = spawnArea;
        move.allowArea = allowArea;
        move.denyArea = denyArea;

        move.SetRandomDirection();

        SwordItem5 click = item.GetComponent<SwordItem5>();

        click.panel = panel;

        // C³•”•ª
        click.quizManager = FindObjectOfType<Quiz5>();
    }
}