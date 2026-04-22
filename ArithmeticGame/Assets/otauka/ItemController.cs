using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject itemPrefab;
    public float minTime = 1f;
    public float maxTime = 3f;

    public Transform canvas; // ← 追加
    public RectTransform spawnArea; // 出現範囲
    public RectTransform allowArea; // 表示OKエリア
    public RectTransform denyArea;  // 表示NGエリア

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
        RectTransform rtSpawn = spawnArea;

        float width = rtSpawn.rect.width;
        float height = rtSpawn.rect.height;

        float x, y;
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // 上
                x = Random.Range(-width / 2, width / 2);
                y = height / 2 + 50f;
                break;

            case 1: // 下
                x = Random.Range(-width / 2, width / 2);
                y = -height / 2 - 50f;
                break;

            case 2: // 左
                x = -width / 2 - 50f;
                y = Random.Range(-height / 2, height / 2);
                break;

            default: // 右
                x = width / 2 + 50f;
                y = Random.Range(-height / 2, height / 2);
                break;
        }

        GameObject item = Instantiate(itemPrefab, spawnArea.parent);

        RectTransform rt = item.GetComponent<RectTransform>();

        // ★ ここが超重要
        rt.anchoredPosition = new Vector2(x, y);

        ItemMove move = item.GetComponent<ItemMove>();
        move.spawnArea = spawnArea;
        move.allowArea = allowArea;
        move.denyArea = denyArea;

        move.SetRandomDirection();
    }
}