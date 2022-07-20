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
    private void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * Speed));
    }

    private void DestroyProjectiles()
    {
        Destroy(gameObject);
    }
    
}

