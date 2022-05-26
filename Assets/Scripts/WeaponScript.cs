using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponScript : MonoBehaviour
{
    private const float MaxStamina = 20f;
    private const float StaminaRefillValue = 0.05f;

    private float currentStamina;
    public float offset;


    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public StaminaBar staminaBar;

    private Animator animator;


    public void setProjectile(GameObject newProjectile)
    {
        projectile = newProjectile;
    }

    public GameObject getProjectile()
    {
        return projectile;
    }

    private void Start()
    {
        currentStamina = MaxStamina;
        staminaBar.setMaxStamina(MaxStamina);
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if ((GetComponentInParent<CharacterController2D>().transform.localScale.x > 0 && difference.x > 0)
                    || (GetComponentInParent<CharacterController2D>().transform.localScale.x < 0 && difference.x < 0))
                {
                    if (StaminaDamage(5))
                    {
                        animator.SetTrigger("Shooting");
                        Instantiate(projectile, shotPoint.position, transform.rotation);
                        timeBtwShots = startTimeBtwShots;
                    }
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (CanStaminaRefill())
        {
            currentStamina = Math.Min(currentStamina + StaminaRefillValue, MaxStamina);
            staminaBar.setStamina(currentStamina);
        }
    }

    private bool CanStaminaRefill()
    {
        return currentStamina < MaxStamina;
    }

    private bool StaminaDamage(int attack)
    {
        if (currentStamina >= 0)
        {
            currentStamina -= attack;
            staminaBar.setStamina(currentStamina);
            return true;
        }

        return false;
    }
}