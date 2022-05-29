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
        gotKey = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            gotKey = true;
            Destroy(GameObject.FindGameObjectWithTag("key"));
            floatingText.GetComponentInChildren<TextMesh>().text = "Acquired city gate key";
            Instantiate(floatingText, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("CityEnterance"))
        {
            if (gotKey)
            {
                floatingText.GetComponentInChildren<TextMesh>().text = "Used key to open the gate";
                Instantiate(floatingText, transform.position, Quaternion.identity);
                Invoke("openCityGate",2f);
            }
            else
            {
                floatingText.GetComponentInChildren<TextMesh>().text = "City gate closed, first find the key";
                Instantiate(floatingText, transform.position, Quaternion.identity);
                //Invoke("openCityGate",2f);
            }

        }
    }

    private void openCityGate()
    {
        gotKey = false;
        Destroy(GameObject.FindGameObjectWithTag("CityEnterance"));
    }
    
}
