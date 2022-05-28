using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeSolidOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Invoke("BecomeSolid",1f);
        }
    }

    private void BecomeSolid()
    {
        gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
    }
}
