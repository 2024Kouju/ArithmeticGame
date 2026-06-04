using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 moveDir;

    public RectTransform spawnArea;


    private SpriteRenderer sr;
    private Camera mainCam;

    private Rigidbody2D rb;
    public float bouncePower = 0.5f;
    private bool isVisible = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        mainCam = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        SetAlpha(1f);

        isVisible = true;

        rb.AddForce(moveDir * speed, ForceMode2D.Impulse);
    }
    public void SetDirection(Vector2 dir)
    {
        moveDir = dir.normalized;
    }
    void FixedUpdate()
    {


        
    }

    public void SetRandomDirection()
    {
        Vector3 center = spawnArea.position;

        moveDir =
            (center - transform.position).normalized;
    }

    // =========================
    // è’ìÀÇ≈îΩéÀ
    // =========================
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hart") ||
            collision.gameObject.CompareTag("Shield") ||
            collision.gameObject.CompareTag("Sword"))
        {
            Vector2 normal = collision.contacts[0].normal;

            moveDir = Vector2.Reflect(moveDir, normal).normalized;

            rb.velocity = moveDir * speed;
        }
    }
    // =========================
    // ÉGÉäÉAîªíË
    // =========================
    void CheckArea()
    {
        Vector2 screenPos =
            RectTransformUtility.WorldToScreenPoint(
                mainCam,
                transform.position
            );

 


      
    }

    void SetAlpha(float alpha)
    {
        Color c = sr.color;

        c.a = alpha;

        sr.color = c;
    }
}