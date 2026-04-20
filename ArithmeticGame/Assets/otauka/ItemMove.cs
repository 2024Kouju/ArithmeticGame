using System.Collections;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDir;

    public RectTransform spawnArea;

    private SpriteRenderer sr;
    private Camera mainCam;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;

        SetAlpha(0f);
        StartCoroutine(Fade(0f, 1f, 0.5f));
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);

        CheckOutOfScreen();
    }

    public void SetRandomDirection()
    {
        Vector3 center = spawnArea.position;
        moveDir = (center - transform.position).normalized;
    }

    // =========================
    // 画面外判定
    // =========================
    void CheckOutOfScreen()
    {
        Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);

        bool isOut =
            viewPos.x < -0.1f || viewPos.x > 1.1f ||
            viewPos.y < -0.1f || viewPos.y > 1.1f;

        if (isOut)
        {
            FadeOutAndDestroy();
        }
    }

    // =========================
    // フェード
    // =========================
    IEnumerator Fade(float from, float to, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, time / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(to);
    }

    void SetAlpha(float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

    public void FadeOutAndDestroy()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(1f, 0f, 0.5f));
        Destroy(gameObject);
    }
}