using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTag : MonoBehaviour
{
    public string targetTag = "Item"; // Á‚µ‚½‚¢‘Šè‚Ìƒ^ƒO

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(collision.gameObject); // ‘Šè‚ğÁ‚·
        }
    }
}
