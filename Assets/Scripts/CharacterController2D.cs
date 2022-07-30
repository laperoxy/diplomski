using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    private const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    [SerializeField] private float m_JumpForce = 400f; // Amount of force added when the player jumps.

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement
    [SerializeField] private bool m_AirControl; // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck; // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck; // A position marking where to check for ceilings
    [SerializeField] private Transform playerLight;

    public HealthBar healthbar;
    
    [Header("Events")] [Space] public UnityEvent OnLandEvent;

    private bool m_FacingRight = true; // For determining which way the player is currently facing.
    private bool m_Grounded; // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Velocity = Vector3.zero;

    private bool firstTime = true;
    private bool isFalling = false;
    private Vector3 prevPos;
    private float highestPos;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        OnLandEvent ??= new UnityEvent();
        prevPos = transform.position;
    }

    private void Update()
    {
        
        if (!m_Grounded)
        {
            if (transform.position.y < prevPos.y && firstTime)
            {
                firstTime = false;
                isFalling = true;
                highestPos = transform.position.y;
            }

            prevPos = transform.position;
        }

        if (m_Grounded && isFalling)
        {
            if (highestPos - transform.position.y > 10)
            {
                healthbar.takeDamage(30);
            }

            isFalling = false;
            firstTime = true;
        }
    }

    private void FixedUpdate()
    {
        var wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        var colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, GroundedRadius, m_WhatIsGround);
        foreach (var colliderElement in colliders)
            if (colliderElement.gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
    }


    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                m_MovementSmoothing);


            if (ShouldRotatePlayer(move)) Flip();
        }

        if (ShouldPlayerJump(jump))
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    private bool ShouldRotatePlayer(float move)
    {
        return (move > 0 && !m_FacingRight) || (move < 0 && m_FacingRight);
    }

    private bool ShouldPlayerJump(bool jump)
    {
        return m_Grounded && jump;
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        RotateOnXAxis();
    }

    private void RotateOnXAxis()
    {
        // Multiply the player's x local scale by -1.
        var theScale = transform.localScale;
        var lightdir = playerLight.rotation;
        
        theScale.x *= -1;
        lightdir.z *= -1;
        
        transform.localScale = theScale;

        playerLight.rotation = lightdir;
    }
}