using System.Collections;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDir;

    public RectTransform spawnArea;
    public RectTransform allowArea;
    public RectTransform denyArea;

    private SpriteRenderer sr;
    private Camera mainCam;

    private bool isVisible = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;

        // 最初は表示
        SetAlpha(1f);
        isVisible = true;
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
        CheckArea();
    }

    public void SetRandomDirection()
    {
        Vector3 center = spawnArea.position;
        moveDir = (center - transform.position).normalized;
    }

    // =========================
    // エリア判定（即表示/非表示）
    // =========================
    void CheckArea()
    {
        Vector2 screenPos =
            RectTransformUtility.WorldToScreenPoint(mainCam, transform.position);

        bool inAllow =
            RectTransformUtility.RectangleContainsScreenPoint(allowArea, screenPos, mainCam);

        bool inDeny =
            RectTransformUtility.RectangleContainsScreenPoint(denyArea, screenPos, mainCam);

        bool shouldBeVisible = inAllow && !inDeny;

        // 状態が変わったときだけ更新（無駄処理＆チカつき防止）
        if (shouldBeVisible != isVisible)
        {
            isVisible = shouldBeVisible;
            SetAlpha(isVisible ? 1f : 0f);
        }
    }

    void SetAlpha(float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}