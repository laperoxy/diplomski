using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerControlNew : NetworkBehaviour
{
    [SerializeField] private float runSpeed = 40f;

    private CharacterController2D controller;
    private Animator animator;
    private FollowPlayerScript followPlayerScript;
    private Camera mainCamera;

    [SerializeField] private NetworkVariable<float> networkXAxisOffset = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<bool> networkJump = new NetworkVariable<bool>();
    [SerializeField] private NetworkVariable<bool> networkShooting = new NetworkVariable<bool>();

    // client chaches positions
    private float cachedClientXAxisOffset;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(-4.53f, 2.0f, 0);
            mainCamera = Camera.main;
            followPlayerScript = mainCamera.GetComponent<FollowPlayerScript>();
            focusCameraOnPlayer();
        }
    }
    
    
    private void focusCameraOnPlayer()
    {
        followPlayerScript.target = transform;
        followPlayerScript.followedGameObject = gameObject;
        mainCamera.orthographicSize = 6.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsClient && IsOwner)
        {
            if (!ReferenceEquals(followPlayerScript.target, transform) ||
                !ReferenceEquals(followPlayerScript.followedGameObject, gameObject))
            {
                focusCameraOnPlayer();
            }
            UpdateClientInput();
        }
        UpdateClientPosition();

    }

    private void UpdateClientPosition()
    {
        controller.Move(networkXAxisOffset.Value * Time.fixedDeltaTime, networkJump.Value);
        UpdateClientVisuals();
        if (IsServer)
        {
            networkJump.Value = false;
            networkShooting.Value = false;
        }
    }

    private void UpdateClientVisuals()
    {
        animator.SetFloat("Speed", Mathf.Abs(networkXAxisOffset.Value));
        animator.SetBool("isJumping",networkJump.Value);
        if (networkShooting.Value)
        {
            animator.SetTrigger("Shooting");
        }
    }

    private void UpdateClientInput()
    {
        var xAxisOffsetVar = Input.GetAxisRaw("Horizontal") * runSpeed;
        var jumpVar = Input.GetButtonDown("Jump");

        if (isValueDifferentFromClientCachedOne(xAxisOffsetVar, jumpVar))
        {
            cachedClientXAxisOffset = xAxisOffsetVar;
            updateClientPositionsServerRpc(xAxisOffsetVar, jumpVar);
        }
    }

    private bool isValueDifferentFromClientCachedOne(float xAxisOffsetVar, bool jumpVar)
    {
        return cachedClientXAxisOffset != xAxisOffsetVar || jumpVar;
    }

    [ServerRpc]
    public void updateClientPositionsServerRpc(float xAxisOffsetVar, bool jumpVar)
    {
        networkXAxisOffset.Value = xAxisOffsetVar;
        networkJump.Value = jumpVar;
    }
    
    public void UpdateShooting()
    {
        updateShootingServerRpc();
    }

    [ServerRpc]
    public void updateShootingServerRpc()
    {
        networkShooting.Value = true;
    }

}