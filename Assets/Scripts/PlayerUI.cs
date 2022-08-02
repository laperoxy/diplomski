using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : NetworkBehaviour
{

    public void StopGame()
    {
        if (IsClient && IsOwner)
        {
            SceneManager.LoadScene(0);
        }
    }
}
