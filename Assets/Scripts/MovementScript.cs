using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator; 

    float horizontalMove = 0f;
    public float runSpeed = 40f;

    bool jump = false;


    public static ProjectileScript ProjectilePrefab;
    public Transform LaunchOffset;

    public int maxHealth = 20;
    public int currentHealth;

    public float maxStamina = 20f;
    public float currentStamina;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    public Rigidbody2D rb;

    public Canvas playerCanvas;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);

        currentStamina = maxStamina;
        staminaBar.setMaxStamina(maxStamina);
    }

    // Update is called once per frame
    // My comment
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(transform.localScale.x == 1)
        {
            LaunchOffset.transform.right = new Vector3(1,0,0);
            playerCanvas.GetComponent<Transform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            LaunchOffset.transform.right = new Vector3(-1, 0, 0);
            playerCanvas.GetComponent<Transform>().localScale = new Vector3(-0.01f, 0.01f, 0.01f);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(StaminaDamage(5))
                Instantiate(ProjectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
        }

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime,false, jump);
        jump = false;

        if (currentStamina + 0.001f < maxStamina)
        {
            currentStamina += 0.05f;
            staminaBar.setStamina(currentStamina);
        }

    }

    public static void setProjectilePrefab(ProjectileScript prefab)
    {
        ProjectilePrefab = prefab;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void getHealthBack(int health)
    {

        if (currentHealth + health < maxHealth)
            currentHealth += health;
        else
            currentHealth = maxHealth;
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


    public bool StaminaDamage(int attack)
    {
        if (currentStamina - attack >= 0)
        {
            currentStamina -= attack;
            staminaBar.setStamina(currentStamina);
            return true;
        }
        else
        {
            return false;
        }
    }

}
