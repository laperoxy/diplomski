using Enums;
using Unity.Netcode;
using UnityEngine;

public class BossScript : NetworkBehaviour
{

    private readonly float MAX_BOSS_HEALTH = 200;
    
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    private BossAttackTypes BossAttackType;

    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();

    void Start()
    {
        audioToPlay = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (IsServer)
        {
            if (col.gameObject.CompareTag("SoulPush"))
            {
                reduceHealth(30);
            }
            else if (col.gameObject.CompareTag("SoulFragment"))
            {
                reduceHealth(5);
            }
        }
    }

    void Update()
    {
        if (IsServer)
        {
            
        }
    }
    
    public void reduceHealth(float healthToLose)
    {
        
        audioToPlay.PlayOneShot(SoundToPlay,Volume);
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
