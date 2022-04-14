using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionWithWaterScript : MonoBehaviour
{

    public Transform playerPos;
    public Vector3 startingPosition;
    private void Start()
    {
        startingPosition = playerPos.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            playerPos.position = startingPosition;
            //Destroy(gameObject);
        }
    }


}
