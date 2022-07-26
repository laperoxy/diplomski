using System;
using Enums;
using Unity.Netcode;
using UnityEngine;

public class BossScript : NetworkBehaviour
{

    private readonly float MAX_BOSS_HEALTH = 200;
    private float COOLDOWN_BETWEEN_ATTACKS = 1.5f;
    
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    private NetworkVariable<BossAttackTypes> networkBossAttackType = new NetworkVariable<BossAttackTypes>();
    private DateTime lastTimeAttackWasDone = new DateTime(0);
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bloodBall;
    [SerializeField] private GameObject fireBall;

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
            if ((DateTime.Now - lastTimeAttackWasDone).TotalSeconds > COOLDOWN_BETWEEN_ATTACKS)
            {
                AttackWithSkill();
                lastTimeAttackWasDone = DateTime.Now;
            }
        }
    }

    private void AttackWithSkill()
    {
        switch (networkBossAttackType.Value)
        {
            case BossAttackTypes.BLOOD_BALL:
                Instantiate(bloodBall, shotPoint.position, Quaternion.Euler(0,0,90)).GetComponent<NetworkObject>().Spawn();
                networkBossAttackType.Value = BossAttackTypes.FIRE_BALL;
                break;
            case BossAttackTypes.FIRE_BALL:
                Instantiate(fireBall, shotPoint.position, Quaternion.Euler(0,0,90)).GetComponent<NetworkObject>().Spawn();
                networkBossAttackType.Value = BossAttackTypes.BLOOD_BALL;
                break;
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
