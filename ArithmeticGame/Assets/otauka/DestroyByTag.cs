using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTag : MonoBehaviour
{
   

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }
}
