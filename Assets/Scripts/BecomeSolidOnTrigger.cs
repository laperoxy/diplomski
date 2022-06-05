using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeSolidOnTrigger : MonoBehaviour
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
            Invoke(nameof(BecomeSolid),0.5f);
        }
    }

    private void BecomeSolid()
    {
        gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
    }
}
