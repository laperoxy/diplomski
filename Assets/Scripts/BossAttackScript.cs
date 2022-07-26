using UnityEngine;

public class BossAttackScript : MonoBehaviour
{
    public readonly float SPEED = 11.0f;
    public float lifeTime;

    private void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
    }
    private void Update()
    {
        transform.Translate(Vector2.up * (Time.deltaTime * SPEED));
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

