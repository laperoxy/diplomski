using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionWithWaterScript : MonoBehaviour
{

    public MovementScript ms;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            ms.TakeDamage(ms.maxHealth);
        }
    }

}
