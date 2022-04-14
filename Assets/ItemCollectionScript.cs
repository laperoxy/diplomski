using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollectionScript : MonoBehaviour
{

    private int coinsCollected = 0;
    [SerializeField] private Text coinsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinsCollected++;
            Destroy(collision.gameObject);
            coinsText.text = "" + coinsCollected;
        }

        if (collision.gameObject.CompareTag("End"))
        {
            Destroy(collision.gameObject);
        }
    }
}
