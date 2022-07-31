using System;
using Enums;
using Unity.Netcode;
using UnityEngine;

public class BossScript : NetworkBehaviour
{

    private readonly float MAX_BOSS_HEALTH = 200;
    private float COOLDOWN_BETWEEN_ATTACKS = 1.5f;
    private Animator animator;
    
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    private NetworkVariable<BossAttackTypes> networkBossAttackType = new NetworkVariable<BossAttackTypes>();
    private NetworkVariable<BossPhases> networkBossPhases = new NetworkVariable<BossPhases>();
    private DateTime lastTimeAttackWasDone = new DateTime(0);
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject bloodBall;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject endgameLight;

    [SerializeField] private GameObject bossClone;
    [SerializeField] private GameObject finalPhase;

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
            if (!IsCooldownActive())
            {
                animator.SetTrigger("boss_attack");
                Invoke(nameof(AttackWithSkill),0.25f);
                //AttackWithSkill();
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
        if (networkBossPhases.Value == BossPhases.FIRST)
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
        else if (networkBossPhases.Value == BossPhases.SECOND)
        {
            Instantiate(fireBall, shotPoint.position, Quaternion.Euler(0,0,90)).GetComponent<NetworkObject>().Spawn();
            networkBossAttackType.Value = BossAttackTypes.BLOOD_BALL;
        }
        else if (networkBossPhases.Value == BossPhases.THIRD)
        {
            Instantiate(fireBall, shotPoint.position, Quaternion.Euler(0,0,90)).GetComponent<NetworkObject>().Spawn();
            networkBossAttackType.Value = BossAttackTypes.BLOOD_BALL;
        }
    }

    public void reduceHealth(float healthToLose)
    {
        animator.SetTrigger("boss_hurt");
        audioToPlay.PlayOneShot(SoundToPlay,Volume);
        networkHealthBar.Value -= healthToLose;
        ProgressToNextPhaseIfNeeded();
        if (networkHealthBar.Value <= 0)
        {
            Instantiate(finalPhase).GetComponent<NetworkObject>().Spawn();
            EndGameScript.FinishGameIfAllBossesAreDead(endgameLight);
            Destroy(gameObject);
        }
    }

    private void ProgressToNextPhaseIfNeeded()
    {
        if (networkBossPhases.Value == BossPhases.FIRST && networkHealthBar.Value < MAX_BOSS_HEALTH * 2 / 3)
        {
            networkBossPhases.Value = BossPhases.SECOND;
        }
        else if (networkBossPhases.Value == BossPhases.SECOND && networkHealthBar.Value < MAX_BOSS_HEALTH / 3)
        {
            networkBossPhases.Value = BossPhases.THIRD;
            Instantiate(bossClone).GetComponent<NetworkObject>().Spawn();
        }
    }
    
    private void Awake()
    {
        SetMaxHealth();
        animator =  GetComponent<Animator>();
    }

    public void SetMaxHealth()
    {
        networkHealthBar.Value = MAX_BOSS_HEALTH;
    }
}
