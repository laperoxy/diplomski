using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public CharacterController2D controller;

    float horizontalMove = 0f;
    public float runSpeed = 40f;

    bool jump = false;


    public static ProjectileScript ProjectilePrefab;
    public Transform LaunchOffset;

    // Update is called once per frame
    // My comment
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(transform.localScale.x == 1)
        {
            LaunchOffset.transform.right = new Vector3(1,0,0);
        }
        else
        {
            LaunchOffset.transform.right = new Vector3(-1, 0, 0);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
        }

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,false, jump);
        jump = false;

    }

    public static void setProjectilePrefab(ProjectileScript prefab)
    {
        ProjectilePrefab = prefab;
    }

}
