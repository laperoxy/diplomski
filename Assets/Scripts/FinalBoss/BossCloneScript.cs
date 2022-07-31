using System;
using Enums;
using Unity.Netcode;
using UnityEngine;

public class BossCloneScript : NetworkBehaviour
{

    private readonly float MAX_BOSS_HEALTH = 100;
    private float COOLDOWN_BETWEEN_ATTACKS = 2.5f;
    
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    private DateTime lastTimeAttackWasDone = new DateTime(0);
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject endgameLight;

    [SerializeField] private NetworkVariable<float> networkHealthBar = new NetworkVariable<float>();

    private Animator animator;

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
        Instantiate(fireBall, shotPoint.position, Quaternion.Euler(0,0,-90)).GetComponent<NetworkObject>().Spawn();
    }

    public void reduceHealth(float healthToLose)
    {
        animator.SetTrigger("boss_hurt");
        audioToPlay.PlayOneShot(SoundToPlay,Volume);
        networkHealthBar.Value -= healthToLose;
        if (networkHealthBar.Value <= 0)
        {
            EndGameScript.FinishGameIfAllBossesAreDead(endgameLight);
            Destroy(gameObject);
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
