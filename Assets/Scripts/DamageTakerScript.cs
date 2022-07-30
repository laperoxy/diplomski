using Unity.Netcode;
using UnityEngine;

public class DamageTakerScript: NetworkBehaviour
{
    
    public AudioClip bloodyPunchSound;
    public AudioClip refillSound;
    public float Volume;
    public AudioSource audioToPlay;
    public HealthBar healthBar;
    public StaminaBar staminabar;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsServer)
        {
            if (col.gameObject.CompareTag("BloodBall"))
            {
                healthBar.takeDamage(5);
                audioToPlay.PlayOneShot(bloodyPunchSound,Volume);
            }
            else if (col.gameObject.CompareTag("FireBall"))
            {
                healthBar.takeDamage(10);
                audioToPlay.PlayOneShot(bloodyPunchSound,Volume);
            }
            else if (col.gameObject.CompareTag("Thorns"))
            {
                healthBar.takeDamage(10);
                audioToPlay.PlayOneShot(bloodyPunchSound,Volume);
            }
            else if (col.gameObject.CompareTag("AcidWaste"))
            {
                healthBar.takeDamage(5);
                audioToPlay.PlayOneShot(bloodyPunchSound,Volume);
            }
            else if (col.gameObject.CompareTag("Heart"))
            {
                if (healthBar.canReplenishHealth())
                {
                    healthBar.heal(20);
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound,Volume);
                }
            }
            else if (col.gameObject.CompareTag("BigHeart"))
            {
                if (healthBar.canReplenishHealth())
                {
                    healthBar.heal(50);
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound,Volume);
                }
            }
            else if (col.gameObject.CompareTag("Stamina"))
            {
                if (staminabar.ReplenishStamina())
                {
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound,Volume);
                }
            }
            else if (col.gameObject.CompareTag("Stamina1"))
            {
                if (staminabar.ReplenishStamina())
                {
                    Destroy(col.gameObject);
                    audioToPlay.PlayOneShot(refillSound,Volume);
                }
            }
        }
    }
}
