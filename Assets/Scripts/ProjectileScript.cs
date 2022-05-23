using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float Speed = 4.5f;
    public float lifeTime;

    private void Start()
    {
        Invoke("DestroyProjectiles",lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.up*Speed*Time.deltaTime);

    }

    void DestroyProjectiles()
    {
        Destroy(gameObject);
    }
    
}

