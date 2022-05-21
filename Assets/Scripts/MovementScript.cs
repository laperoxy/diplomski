using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    private const int MaxHealth = 20;

    private const float MaxStamina = 20f;

    private const float StaminaRefillValue = 0.05f;
    private static ProjectileScript s_ProjectilePrefab;

    public CharacterController2D controller;
    public Animator animator;

    public float horizontalMove;
    public float runSpeed = 40f;
    public Transform LaunchOffset;
    public int currentHealth;
    public float currentStamina;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    public Rigidbody2D rb;

    public Canvas playerCanvas;

    private bool jump;

    private void Start()
    {
        currentHealth = MaxHealth;
        healthBar.setMaxHealth(MaxHealth);

        currentStamina = MaxStamina;
        staminaBar.setMaxStamina(MaxStamina);
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump")) jump = true;

        if (transform.localScale.x == 1)
        {
            LaunchOffset.transform.right = new Vector3(1, 0, 0);
            playerCanvas.GetComponent<Transform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            LaunchOffset.transform.right = new Vector3(-1, 0, 0);
            playerCanvas.GetComponent<Transform>().localScale = new Vector3(-0.01f, 0.01f, 0.01f);
        }

        if (Input.GetButtonDown("Fire1"))
            if (StaminaDamage(5))
                Instantiate(s_ProjectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        if (CanStaminaRefill())
        {
            currentStamina = Math.Min(currentStamina + StaminaRefillValue, MaxStamina);
            staminaBar.setStamina(currentStamina);
        }
    }

    private bool CanStaminaRefill()
    {
        return currentStamina < MaxStamina;
    }

    public static void setProjectilePrefab(ProjectileScript prefab)
    {
        s_ProjectilePrefab = prefab;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        dieIfZeroHealth();
    }

    public void TakeWaterDamage()
    {
        currentHealth = 0;
        healthBar.setHealth(currentHealth);
        dieIfZeroHealth();
    }

    private void dieIfZeroHealth()
    {
        if (currentHealth <= 0) Die();
    }

    public void getHealthBack(int health)
    {
        if (currentHealth + health < MaxHealth)
            currentHealth += health;
        else
            currentHealth = MaxHealth;
        healthBar.setHealth(currentHealth);
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private bool StaminaDamage(int attack)
    {
        if (currentStamina - attack >= 0)
        {
            currentStamina -= attack;
            staminaBar.setStamina(currentStamina);
            return true;
        }

        return false;
    }
}