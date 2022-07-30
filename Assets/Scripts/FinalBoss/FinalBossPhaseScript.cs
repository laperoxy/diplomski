using System;
using Enums;
using Unity.Netcode;
using UnityEngine;

public class FinalBossPhaseScript : NetworkBehaviour
{

    private readonly float MAX_BOSS_HEALTH = 100;
    private float COOLDOWN_BETWEEN_ATTACKS = 6f;
    private readonly int MAX_SPAWNED_OBJECTS = 4;
    
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    private DateTime lastTimeAttackWasDone = new DateTime(0);
    private int state = 0;
    private int spawnedObjects = 0;
    [SerializeField] private GameObject leftAcidBall;
    [SerializeField] private GameObject rightAcidBall;

    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();

    void Start()
    {
        audioToPlay = GetComponent<AudioSource>();
        lastTimeAttackWasDone = DateTime.Now;
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
            if (!IsCooldownActive())
            {
                AttackWithSkill();
                lastTimeAttackWasDone = DateTime.Now;
            }
        }
    }

    private bool IsCooldownActive()
    {
        return (DateTime.Now - lastTimeAttackWasDone).TotalSeconds <= COOLDOWN_BETWEEN_ATTACKS;
    }


    private void AttackWithSkill()
    {
        SpawnAcidBall();
    }

    private void SpawnAcidBall()
    {
        if (state == 0)
            if (spawnedObjects < MAX_SPAWNED_OBJECTS)
            {
                Instantiate(rightAcidBall).GetComponent<NetworkObject>().Spawn();
            }
            else
            {
                Instantiate(leftAcidBall).GetComponent<NetworkObject>().Spawn();
            }

        state = (state + 1) % 2;
        if (state == 0)
        {
            Instantiate(rightAcidBall).GetComponent<NetworkObject>().Spawn();
        }
        else
        {
            Instantiate(leftAcidBall).GetComponent<NetworkObject>().Spawn();
        }

        state = (state + 1) % 2;
        ++spawnedObjects;
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
        SetMaxHealth();
    }

    public void SetMaxHealth()
    {
        networkHealthBar.Value = MAX_BOSS_HEALTH;
    }
}
