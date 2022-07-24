using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BossScript : NetworkBehaviour
{

    private readonly float MAX_BOSS_HEALTH = 100;
    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SoulPush"))
        {
            reduceHealthServerRpc(20);
        }
        else if (col.gameObject.CompareTag("SoulFragment"))
        {
            reduceHealthServerRpc(50);
        }
    }

    [ServerRpc]
    public void reduceHealthServerRpc(float healthToLose)
    {
        networkHealthBar.Value -= healthToLose;
        if (networkHealthBar.Value <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void Awake()
    {
        SetMaxStamina();
    }

    public void SetMaxStamina()
    {
        networkHealthBar.Value = MAX_BOSS_HEALTH;
    }
}
