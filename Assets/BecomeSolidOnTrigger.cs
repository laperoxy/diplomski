using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeSolidOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GroundCheck"))
        {
            Invoke("BecomeSolid",0.5f);
        }
    }

    private void BecomeSolid()
    {
        gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
    }
}
