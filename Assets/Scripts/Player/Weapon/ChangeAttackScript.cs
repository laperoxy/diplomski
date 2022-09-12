using System;
using Enums;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAttackScript : NetworkBehaviour
{
    [SerializeField] private Text attackSwitch;
    [SerializeField] private Image attack;
    
    [SerializeField] private Sprite soulPushSprite;
    [SerializeField] private Sprite soulFragmentSprite;

    [SerializeField]
    private WeaponScript weaponScript;

    private void Start()
    {
        if (IsClient && !IsOwner)
        {
            Destroy(attackSwitch);
            Destroy(attack);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner && Input.GetKeyDown(KeyCode.E))
        {
            switch (weaponScript.switchProjectile())
            {
                case WeaponTypes.SOUL_PUSH:
                    attackSwitch.text = "Soul Push";
                    attack.sprite = soulPushSprite;
                    break;
                case WeaponTypes.SOUL_FRAGMENT:
                    attackSwitch.text = "Soul fragment";
                    attack.sprite = soulFragmentSprite;
                    break;
            }
        }
    }
}