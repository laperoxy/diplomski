using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerControlNew : NetworkBehaviour
{
    [SerializeField] private float runSpeed = 40f;

    private CharacterController2D controller;
    private Animator animator;

    [SerializeField] private NetworkVariable<float> networkXAxisOffset = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<bool> networkJump = new NetworkVariable<bool>();

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
            transform.position = new Vector3(-4, 0, 0);
            focusCameraOnPlayer();
        }
    }
    
    
    private void focusCameraOnPlayer()
    {
        Camera.main.GetComponent<FollowPlayerScript>().target = transform;
        Camera.main.orthographicSize = 6.0f;

    }

    // Update is called once per frame
    private void Update()
    {
        if (IsClient && IsOwner) UpdateClientInput();
        UpdateClientPosition();

    }

    private void UpdateClientPosition()
    {
        controller.Move(networkXAxisOffset.Value * Time.fixedDeltaTime, networkJump.Value);
        UpdateClientVisuals();
        if (IsServer)
        {
            networkJump.Value = false;
        }
    }

    private void UpdateClientVisuals()
    {
        animator.SetFloat("Speed", Mathf.Abs(networkXAxisOffset.Value));
        animator.SetBool("isJumping",networkJump.Value);
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

}