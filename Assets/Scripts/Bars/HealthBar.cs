using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{
    private const float MAX_HEALTH = 100f;

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();


    private void Awake()
    {
        SetMaxHealth();
    }

    private void Update()
    {
        UpdateHealthStatus();
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
            gameObject.GetComponentsInParent<PlayerControlNew>()[0].transform.position = new Vector3(-4.53f, 2.0f, 0);
            networkHealthBar.Value = MAX_HEALTH;
        }
    }
    public void takeDamage(float damage)
    {
        networkHealthBar.Value = Math.Max(networkHealthBar.Value - damage, 0);
    }
    
    public void heal(float healValue)
    {
        networkHealthBar.Value = Math.Max(networkHealthBar.Value + healValue, MAX_HEALTH);
    }
    public bool canReplenishHealth()
    {
        return networkHealthBar.Value < MAX_HEALTH;
    }
}