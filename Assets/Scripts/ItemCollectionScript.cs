using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionScript : MonoBehaviour
{
    [SerializeField] private bool gotKey;
    public GameObject floatingText;
    private float lastPopupTime; //time in seconds
    private const float POPUP_COOLDOWN = 2; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("key"))
        {
            gotKey = true;
            Destroy(GameObject.FindGameObjectWithTag("key"));
            SetAndShowText("Acquired city gate key");
            Destroy(GameObject.FindGameObjectWithTag("ClockTower"));
        }

        if (collision.gameObject.CompareTag("ClockTower"))
        {
            SetAndShowText("Looks like the door is locked \n" +
                        "I will have to find another way to the top");
        }

        if (!gotKey && collision.gameObject.CompareTag("ClockTowerSign"))
        {
            SetAndShowText("There is the abandoned Clock tower \n" +
                           "Looks like there is something shiny on top");
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("CityEnterance"))
        {
            if (gotKey)
            {
                SetAndShowText("Used key to open the gate");
                OpenCityGate();
            }
            else
            {
                SetAndShowText("City gate closed, first find the key");
            }
        }
    }

    private void OpenCityGate()
    {
        gotKey = false;
        Destroy(GameObject.FindGameObjectWithTag("CityEnterance"));
    }

    private void updatePopupTime()
    {
        lastPopupTime = Time.time;
    }
    
    private void SetAndShowText(String text)
    {
        if (shouldShowText())
        {
            floatingText.GetComponentInChildren<TextMesh>().text = text;
            Instantiate(floatingText, transform.position, Quaternion.identity);
            updatePopupTime();
        }
    }

    private bool shouldShowText()
    {
        return Time.time - POPUP_COOLDOWN > lastPopupTime;
    }
}