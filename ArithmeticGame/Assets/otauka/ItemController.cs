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
        // Imageの四隅を取得（ワールド座標）
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);

        // 左下と右上
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        // 範囲内でランダム
        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);

        Vector2 spawnPos = new Vector2(x, y);

        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

        // 動きつける
        item.GetComponent<ItemMove>().spawnArea = spawnArea;
        item.GetComponent<ItemMove>().SetRandomDirection();
    }
}
