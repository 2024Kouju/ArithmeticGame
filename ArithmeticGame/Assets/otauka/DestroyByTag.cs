using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTag : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hart"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Sword"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Shield"))
        {
            Destroy(other.gameObject);
        }
    }
}
