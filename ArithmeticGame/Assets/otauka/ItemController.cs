using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject itemPrefab;
    public float minTime = 1f;
    public float maxTime = 3f;

    public RectTransform spawnArea; // ← Imageを入れる！

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

            SpawnItem();
        }
    }

    void SpawnItem()
    {
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        float x, y;

        // どの方向から出すか（0:上 1:下 2:左 3:右）
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // 上
                x = Random.Range(bottomLeft.x, topRight.x);
                y = topRight.y + 1f;
                break;

            case 1: // 下
                x = Random.Range(bottomLeft.x, topRight.x);
                y = bottomLeft.y - 1f;
                break;

            case 2: // 左
                x = bottomLeft.x - 1f;
                y = Random.Range(bottomLeft.y, topRight.y);
                break;

            default: // 右
                x = topRight.x + 1f;
                y = Random.Range(bottomLeft.y, topRight.y);
                break;
        }

        Vector2 spawnPos = new Vector2(x, y);

        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        // 中心に向かう
        item.GetComponent<ItemMove>().spawnArea = spawnArea;
        item.GetComponent<ItemMove>().SetRandomDirection();
    }
}
