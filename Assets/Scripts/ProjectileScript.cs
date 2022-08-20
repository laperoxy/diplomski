using System;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float lifeTime;
    [SerializeField]
    private float speed = 11.0f;

    private void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * speed));
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Boss") || col.gameObject.CompareTag("MiniBoss"))
        {
            DestroyProjectile();
        }
    }
}

