using UnityEngine;

public class CollisionWithWaterScript : MonoBehaviour
{
    public MovementScript ms;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("End")) ms.TakeWaterDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("End")) ms.TakeWaterDamage();
    }
}