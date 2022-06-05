using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPillarScript : MonoBehaviour
{
    private GameObject pillar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pillar==null)
        {
            pillar = GameObject.FindWithTag("PillarToDestroy");
            pillar.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Invoke(nameof(DestroyPillarObject),2f);
        }
    }

    private void DestroyPillarObject()
    {
        Destroy(pillar);
    }
}
