using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPillarScript : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject followedGameObject = mainCamera.GetComponent<FollowPlayerScript>().followedGameObject;
        if (collision.gameObject.Equals(followedGameObject))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Invoke(nameof(DestroyObject),2f);
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
