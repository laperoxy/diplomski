using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPillarScript : MonoBehaviour
{

    public Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            Invoke("destroyObject",2f);
        }
    }

    private void destroyObject()
    {
        Destroy(gameObject);
    }
}
