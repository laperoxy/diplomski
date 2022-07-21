using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{
    private const float MAX_HEALTH = 20f;

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
        if (IsClient && !IsOwner)
        {
            slider.value = networkHealthBar.Value;
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
    void updateHealthServerRpc(float stamina)
    {
        networkHealthBar.Value = stamina;
    }
}