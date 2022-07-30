using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : NetworkBehaviour
{
    private const float MAX_STAMINA = 100f;
    private const float STAMINA_REFILL_VALUE = 0.25f;

    // slider has functionality of cached stamina float field
    public Slider slider;

    [SerializeField] private NetworkVariable<float> networkStaminaBar = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<int> networkStaminaPacks = new NetworkVariable<int>();

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
        networkStaminaPacks.Value = 0;
    }

    private void RefillStaminaAndUpdate()
    {
        if (IsClient && IsOwner)
        {
            if (CanStaminaRefill())
            {
                bool shouldUseStaminaPack = doesHaveStaminaPack();
                slider.value = shouldUseStaminaPack
                    ? MAX_STAMINA
                    : Math.Min(slider.value + STAMINA_REFILL_VALUE, MAX_STAMINA);
                updateStaminaServerRpc(slider.value, shouldUseStaminaPack);
            }
        }
        else if (IsClient && !IsOwner)
        {
            slider.value = networkStaminaBar.Value;
        }
    }

    private bool doesHaveStaminaPack()
    {
        return networkStaminaPacks.Value != 0;
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
            updateStaminaServerRpc(slider.value, false);
        }
    }

    public bool ReplenishStamina()
    {
        if (FloatComparator.NotEqual(networkStaminaBar.Value, MAX_STAMINA))
        {
            networkStaminaPacks.Value += 1;
            return true;
        }

        return false;
    }

    [ServerRpc]
    void updateStaminaServerRpc(float stamina, bool didUseStaminaPack)
    {
        networkStaminaBar.Value = stamina;
        if (didUseStaminaPack)
        {
            networkStaminaPacks.Value = Math.Max(0, networkStaminaPacks.Value - 1);
        }
    }
}