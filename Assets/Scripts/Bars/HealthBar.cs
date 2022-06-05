using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }
}