using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScript : MonoBehaviour
{
    public HealthBar hb;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Void"))
        {
            if (hb != null)
                hb.setHealth(0);
        }
    }
}