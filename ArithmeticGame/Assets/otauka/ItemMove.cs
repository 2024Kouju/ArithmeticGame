using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDir;

    public RectTransform spawnArea; // Å© í«â¡

    public void SetRandomDirection()
    {
        Vector2 center = Vector2.zero;
        moveDir = (center - (Vector2)transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);

        CheckOutOfArea();
    }

    void CheckOutOfArea()
    {
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        Vector3 pos = transform.position;

        // îÕàÕäOÇ»ÇÁçÌèú
        if (pos.x < bottomLeft.x || pos.x > topRight.x ||
            pos.y < bottomLeft.y || pos.y > topRight.y)
        {
            Destroy(gameObject);
        }
    }
}