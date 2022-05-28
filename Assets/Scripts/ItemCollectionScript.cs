using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionScript : MonoBehaviour
{

    private bool gotKey;
    public GameObject floatingText;
    
    private void Start()
    {
        gotKey = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            gotKey = true;
            Destroy(GameObject.FindGameObjectWithTag("key"));
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("CityEnterance") && gotKey)
        {
            Instantiate(floatingText, transform.position, Quaternion.identity);
            Invoke("openCityGate",2f);
        }
    }

    private void openCityGate()
    {
        gotKey = false;
        Destroy(GameObject.FindGameObjectWithTag("CityEnterance"));
    }
    
}
