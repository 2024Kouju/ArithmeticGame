using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject itemPrefab;
    public float minTime = 1f;
    public float maxTime = 3f; 
    public RectTransform spawnArea; 
    // 出現範囲
    public RectTransform allowArea; 
    // 表示OKエリア
    public RectTransform denyArea;
    // 表示NGエリア]
    public GameObject panel; // ← これ追加
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
        int side = Random.Range(0, 4);
        switch (side) 
        { 
            case 0: 
                // 上
                x = Random.Range(bottomLeft.x, topRight.x);
                y = topRight.y + 1f;
                break;
            case 1:
                // 下
                x = Random.Range(bottomLeft.x, topRight.x); 
                y = bottomLeft.y - 1f; 
                break; 
            case 2: 
                // 左
                x = bottomLeft.x - 1f;
                y = Random.Range(bottomLeft.y, topRight.y); 
                break; 
            default: 
                // 右
                x = topRight.x + 1f;
                y = Random.Range(bottomLeft.y, topRight.y); 
                break; 
        }
        Vector2 spawnPos = new Vector2(x, y); 
        GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity); 
        // ★ ここでPrefabに渡す（重要）
        ItemMove move = item.GetComponent<ItemMove>(); 
        move.spawnArea = spawnArea; 
        move.allowArea = allowArea;
        move.denyArea = denyArea; 
        move.SetRandomDirection();

        // ★ これ追加
        Item click = item.GetComponent<Item>();
        click.panel = panel;

        // ★追加
        click.quizManager = FindObjectOfType<QuizManager>();
    }
}