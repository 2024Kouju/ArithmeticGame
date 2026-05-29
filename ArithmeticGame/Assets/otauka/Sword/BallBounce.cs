using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bouncePower = 5f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ‘Šè‚àBall‚¾‚Á‚½‚ç
        if (collision.gameObject.CompareTag("Item"))
        {
            // Õ“Ë‘Šè‚Æ‚Ì•ûŒü
            Vector2 dir = (transform.position - collision.transform.position).normalized;

            // ‹t•ûŒü‚Ö—Í‚ğ‰Á‚¦‚é
            rb.velocity = dir * bouncePower;
        }
    }
}