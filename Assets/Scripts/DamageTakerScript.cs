using Unity.Netcode;
using UnityEngine;

public class DamageTakerScript: NetworkBehaviour
{
    
    public AudioClip SoundToPlay;
    public float Volume;
    public AudioSource audioToPlay;
    public HealthBar healthBar;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsServer)
        {
            if (col.gameObject.CompareTag("BloodBall"))
            {
                healthBar.takeDamage(5);
                audioToPlay.PlayOneShot(SoundToPlay,Volume);
            }
            else if (col.gameObject.CompareTag("FireBall"))
            {
                healthBar.takeDamage(10);
                audioToPlay.PlayOneShot(SoundToPlay,Volume);
            }
            else if (col.gameObject.CompareTag("Thorns"))
            {
                healthBar.takeDamage(10);
                audioToPlay.PlayOneShot(SoundToPlay,Volume);
            }
            else if (col.gameObject.CompareTag("AcidWaste"))
            {
                healthBar.takeDamage(5);
                audioToPlay.PlayOneShot(SoundToPlay,Volume);
            }else if (col.gameObject.CompareTag("Heart"))
            {
                if (healthBar.canTake())
                {
                    healthBar.takeDamage(-20);
                    Destroy(GameObject.FindGameObjectWithTag("Heart"));
                }
                
            }
            else if (col.gameObject.CompareTag("BigHeart"))
            {
                if (healthBar.canTake())
                {
                    healthBar.takeDamage(-50);
                    Destroy(GameObject.FindGameObjectWithTag("BigHeart"));
                }
            }
        }
    }
}
