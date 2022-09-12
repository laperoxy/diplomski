using Unity.Netcode;
using UnityEngine;

public class DamageTakerScript : NetworkBehaviour
{
    public AudioClip bloodyPunchSound;
    public AudioClip refillSound;
    public AudioClip thornsSound;
    public AudioClip acidSound;
    public float Volume;
    public AudioSource audioToPlay;
    public HealthBar healthBar;
    public StaminaBar staminabar;

    private Animator animator;

    public bool dont_take_damage = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsServer)
        {
            if (col.gameObject.CompareTag("BloodBall"))
            {
                if (!dont_take_damage)
                {
                    healthBar.takeDamage(5);
                    animator.SetTrigger("Hit");
                }

                audioToPlay.PlayOneShot(bloodyPunchSound, Volume);
            }
            else if (col.gameObject.CompareTag("FireBall"))
            {
                if (!dont_take_damage)
                {
                    healthBar.takeDamage(10);
                    animator.SetTrigger("Hit");
                }

                audioToPlay.PlayOneShot(bloodyPunchSound, Volume);
            }
            else if (col.gameObject.CompareTag("boss_third_attack"))
            {
                if (!dont_take_damage)
                {
                    healthBar.takeDamage(15);
                    animator.SetTrigger("Hit");
                }

                audioToPlay.PlayOneShot(bloodyPunchSound, Volume);
            }
            else if (col.gameObject.CompareTag("Thorns"))
            {
                if (!dont_take_damage)
                {
                    healthBar.takeDamage(3);
                    animator.SetTrigger("Hit");
                }

                audioToPlay.PlayOneShot(thornsSound, Volume);
            }
            else if (col.gameObject.CompareTag("AcidWaste"))
            {
                if (!dont_take_damage)
                {
                    healthBar.takeDamage(4);
                    animator.SetTrigger("Hit");
                }

                audioToPlay.PlayOneShot(acidSound, 0.8f);
            }
            else if (col.gameObject.CompareTag("Heart"))
            {
                if (healthBar.canReplenishHealth())
                {
                    healthBar.heal(20);
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound, Volume);
                }
            }
            else if (col.gameObject.CompareTag("BigHeart"))
            {
                if (healthBar.canReplenishHealth())
                {
                    healthBar.heal(50);
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound, Volume);
                }
            }
            else if (col.gameObject.CompareTag("Stamina"))
            {
                if (staminabar.ReplenishStamina())
                {
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound, Volume);
                }
            }
        }
    }
}