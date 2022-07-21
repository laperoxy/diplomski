using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : NetworkBehaviour
{
    
    private const float MAX_STAMINA = 20f;
    private const float STAMINA_REFILL_VALUE = 0.05f;
    
    // slider has functionality of cached stamina float field
    public Slider slider;

    [SerializeField] private NetworkVariable<float> networkStaminaBar = new NetworkVariable<float>();

    private void Awake()
    {
        SetMaxStamina();
    }
    private void FixedUpdate()
    {
        RefillStaminaAndUpdate();
    }

    public void SetMaxStamina()
    {
        slider.maxValue = MAX_STAMINA;
        slider.value = MAX_STAMINA;
        networkStaminaBar.Value = MAX_STAMINA;
    }

    private void RefillStaminaAndUpdate()
    {
        if (IsClient && IsOwner && CanStaminaRefill())
        {
            slider.value = Math.Min(slider.value + STAMINA_REFILL_VALUE, MAX_STAMINA);
            updateStaminaServerRpc(slider.value);
        }
        else if (IsClient && !IsOwner)
        {
            slider.value = networkStaminaBar.Value;
        }
    }
    
    private bool CanStaminaRefill()
    {
        return slider.value < MAX_STAMINA;
    }

    public bool CanShootBullet(int attack)
    {
        return slider.value - attack >= 0;
    }
    public void ShootBullet(int attack)
    {
        if (IsClient && IsOwner && CanShootBullet(attack))
        {
            slider.value -= attack;
            updateStaminaServerRpc(slider.value);
        }
    }

    [ServerRpc]
    void updateStaminaServerRpc(float stamina)
    {
        networkStaminaBar.Value = stamina;
    }
}
