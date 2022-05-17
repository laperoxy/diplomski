using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float Speed = 4.5f;
    public Animator animator;
    public Rigidbody2D rb;
    private bool done;

    private void Start()
    {
        done = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!done)
            transform.position += transform.right * Time.deltaTime * Speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Explosion");
        done = true;
    }

    private void destroyProjectile()
    {
        Destroy(gameObject);
    }
}

