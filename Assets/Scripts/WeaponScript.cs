using Unity.Netcode;
using UnityEngine;

public class WeaponScript : NetworkBehaviour
{

    private const int SHOOTING_STAMINA_COST = 2;

    private const float SHOOTING_OFFSET = -90;


    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public StaminaBar staminaBar;

    private Animator animator;

    [SerializeField] private GameObject player;


    public void setProjectile(GameObject newProjectile)
    {
        projectile = newProjectile;
    }

    public GameObject getProjectile()
    {
        return projectile;
    }

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + SHOOTING_OFFSET);

        if (ProperTime())
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && staminaBar.CanShootBullet(SHOOTING_STAMINA_COST) && IsPlayerShootingInRightDirection(difference))
            {
                staminaBar.ShootBullet(SHOOTING_STAMINA_COST);
                animator.SetTrigger("Shooting");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
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