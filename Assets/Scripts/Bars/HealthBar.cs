using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : NetworkBehaviour
{
    private const float MAX_HEALTH = 100f;

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<bool> networkDisconnected = new NetworkVariable<bool>(false);


    private void Awake()
    {
        SetMaxHealth();
        InvokeRepeating(nameof(DisconnectIfNeeded), 5, 1);
    }

    private void Update()
    {
        UpdateHealthStatus();
    }

    private void DisconnectIfNeeded()
    {
        if (IsOwner && !IsHost && networkDisconnected.Value)
        {
            NetworkManager.Singleton.Shutdown(true);
            SceneManager.LoadScene(0);
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
        validateHealth(health);
        networkHealthBar.Value = health;
        if (networkHealthBar.Value <= 0)
        {
            ReturnDeadPlayerToStartingPosition();
        }
    }
    
    private void validateHealth(float health)
    {
        if (health > 40) throw new ArgumentException();
    }

    private void ReturnDeadPlayerToStartingPosition()
    {
        var positionX = gameObject.transform.position.x;
        gameObject.GetComponentsInParent<PlayerControlNew>()[0].transform.position = new Vector3(-4.53f, 2.0f, 0);
        networkHealthBar.Value = MAX_HEALTH / 10;
        
        if (positionX > 183)
        {
            networkDisconnected.Value = true;
            EndGameScript.FinishGameIfAllBossesAreDead(null);
        }
    }

    public void takeDamage(float damage)
    {
        networkHealthBar.Value = Math.Max(networkHealthBar.Value - damage, 0);
        if (networkHealthBar.Value == 0)
        {
            ReturnDeadPlayerToStartingPosition();
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