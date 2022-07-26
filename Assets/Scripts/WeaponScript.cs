using Enums;
using Unity.Netcode;
using UnityEngine;

public class WeaponScript : NetworkBehaviour
{

    private int SHOOTING_STAMINA_COST = 30;

    private const float SHOOTING_OFFSET = -90;

    private const float COOLDOWN_BETWEEN_SHOTS = 0.5f;
    
    public GameObject soulPush;
    public GameObject soulFragment;

    private WeaponTypes weaponType;
    public Transform shotPoint;

    private float timeBtwShots;

    public StaminaBar staminaBar;

    public PlayerControlNew playerControlNew;

    [SerializeField] private GameObject player;

    public WeaponTypes switchProjectile()
    {
        if (IsClient && IsOwner)
        {
            switch (weaponType)
            {
                case WeaponTypes.SOUL_PUSH:
                    weaponType = WeaponTypes.SOUL_FRAGMENT;
                    SHOOTING_STAMINA_COST = 5;
                    return WeaponTypes.SOUL_FRAGMENT;
                case WeaponTypes.SOUL_FRAGMENT:
                    weaponType = WeaponTypes.SOUL_PUSH;
                    SHOOTING_STAMINA_COST = 30;
                    return WeaponTypes.SOUL_PUSH;
            }
        }

        return WeaponTypes.NO_TYPE;
    }

    void Update()
    {
        if (IsClient && IsOwner)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + SHOOTING_OFFSET);

            if (IsProperTime())
            {
                if ((Input.GetMouseButtonDown(0)) &&
                    staminaBar.CanShootBullet(SHOOTING_STAMINA_COST) && IsPlayerShootingInRightDirection(difference))
                {
                    staminaBar.ShootBullet(SHOOTING_STAMINA_COST);
                    playerControlNew.UpdateShooting();
                    ShootBulletServerRpc(weaponType,shotPoint.position, transform.rotation);
                    timeBtwShots = COOLDOWN_BETWEEN_SHOTS;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private bool IsPlayerShootingInRightDirection(Vector3 difference)
    {
        var localScaleX = player.transform.localScale.x;
        return (localScaleX > 0 && difference.x > 0) || (localScaleX < 0 && difference.x < 0);
    }

    private bool IsProperTime()
    {
        return timeBtwShots <= 0;
    }

    [ServerRpc]
    public void ShootBulletServerRpc(WeaponTypes sentWeaponType, Vector3 shotPointPosition, Quaternion rotation)
    {
        GameObject projectile = sentWeaponType == WeaponTypes.SOUL_PUSH ? soulPush : soulFragment;
        Instantiate(projectile, shotPointPosition, rotation).GetComponent<NetworkObject>().Spawn();
    }
}