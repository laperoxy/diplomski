using System;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public readonly float SPEED = 11.0f;
    public float lifeTime;

    private void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * SPEED));
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Boss"))
        {
            DestroyProjectile();
        }
    }
}

