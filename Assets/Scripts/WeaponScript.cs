using Unity.Netcode;
using UnityEngine;

public class WeaponScript : NetworkBehaviour
{

    private const int SHOOTING_STAMINA_COST = 2;

    private const float SHOOTING_OFFSET = -90;

    private const float COOLDOWN_BETWEEN_SHOTS = 0.5f;


    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;

    public StaminaBar staminaBar;

    public PlayerControlNew playerControlNew;

    [SerializeField] private GameObject player;

    public bool getAndSetProjectile(GameObject currentType, GameObject wantedType)
    {
        if (IsClient && IsOwner)
        {
            if (projectile.Equals(currentType))
            {
                projectile = wantedType;
                return true;
            }
        }

        return false;
    }

    void Update()
    {
        if (IsClient && IsOwner)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + SHOOTING_OFFSET);

            if (ProperTime())
            {
                if ((Input.GetMouseButtonDown(0)) &&
                    staminaBar.CanShootBullet(SHOOTING_STAMINA_COST) && IsPlayerShootingInRightDirection(difference))
                {
                    staminaBar.ShootBullet(SHOOTING_STAMINA_COST);
                    playerControlNew.UpdateShooting();
                    Instantiate(projectile, shotPoint.position, transform.rotation);
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

    private bool ProperTime()
    {
        return timeBtwShots <= 0;
    }
}