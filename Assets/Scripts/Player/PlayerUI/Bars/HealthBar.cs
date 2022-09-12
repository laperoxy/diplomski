using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{
    private const float MAX_HEALTH = 100f;

    public GameObject player;
    public GameObject weapon;

    public GameObject YouLose;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    Material material;
    bool isDissolving;
    float fade = 1f;

    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();


    private void Awake()
    {
        SetMaxHealth();
        isDissolving = false;
        material = player.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        UpdateHealthStatus();
        if (isDissolving)
        {
            fade -= Time.deltaTime/3;
            if (fade <= 0f)
                fade = 0f;

            material.SetFloat("_Fade", fade);
        }
    }

    private void UpdateHealthStatus()
    {
        if (IsClient)
        {
            if (!IsOwner)
            {
                slider.value = networkHealthBar.Value;
            }
            else
            {
                if (FloatComparator.NotEqual(slider.value,networkHealthBar.Value))
                {
                    slider.value = networkHealthBar.Value;
                }
            }

            
        }
    }

    private void SetMaxHealth()
    {
        slider.maxValue = MAX_HEALTH;
        slider.value = MAX_HEALTH;
        networkHealthBar.Value = MAX_HEALTH;
    }

    public void Die()
    {
        if (IsClient && IsOwner)
        {
            slider.value = 0;
            
            updateHealthServerRpc(0);
        }
    }
    
    [ServerRpc]
    void updateHealthServerRpc(float health)
    {
        networkHealthBar.Value = health;
        if (networkHealthBar.Value <= 0)
        {
            isDissolving = true;
            //ReturnDeadPlayerToStartingPosition();
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            weapon.SetActive(false);
            YouLose.SetActive(true);
        }
    }

    private void ReturnDeadPlayerToStartingPosition()
    {
        gameObject.GetComponentsInParent<PlayerControlNew>()[0].transform.position = new Vector3(-4.53f, 2.0f, 0);
        networkHealthBar.Value = MAX_HEALTH / 10;
    }

    public void takeDamage(float damage)
    {
        networkHealthBar.Value = Math.Max(networkHealthBar.Value - damage, 0);
        if (networkHealthBar.Value == 0)
        {
            isDissolving = true;
            //ReturnDeadPlayerToStartingPosition();
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            weapon.SetActive(false);
            YouLose.SetActive(true);
        }
    }
    
    
    
    public void heal(float healValue)
    {
        networkHealthBar.Value = Math.Min(networkHealthBar.Value + healValue, MAX_HEALTH);
    }
    public bool canReplenishHealth()
    {
        return networkHealthBar.Value < MAX_HEALTH;
    }
}