using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    private float health = 100;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SoulPush"))
        {
            health -= 20;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
