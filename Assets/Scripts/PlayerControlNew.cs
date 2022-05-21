using Unity.Netcode;
using UnityEngine;

public class PlayerControlNew : NetworkBehaviour
{
    [SerializeField] private float runSpeed = 40f;

    public CharacterController2D controller;

    [SerializeField] private NetworkVariable<float> xAxisOffset = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<bool> jump = new NetworkVariable<bool>();

    // client chaches positions
    private float cachedClientXAxisOffset;

    private void Start()
    {
        transform.position = new Vector3(0.93f, 0, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsClient && IsOwner) UpdateClientInput();
        UpdateClientPosition();
    }

    private void UpdateClientPosition()
    {
        // if (xAxisOffset.Value != 0 || jump.Value)
        // {
            controller.Move(xAxisOffset.Value * Time.fixedDeltaTime, jump.Value);
            if (IsServer)
            {
                jump.Value = false;
            }
        // }
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
        xAxisOffset.Value = xAxisOffsetVar;
        jump.Value = jumpVar;
    }
}