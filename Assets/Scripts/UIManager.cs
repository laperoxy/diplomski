using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class UIManager : NetworkBehaviour
{
    public List<GameObject> SpawnableObjects;
    private void Awake()
    {
        Cursor.visible = true;
    }

    private void Start() {
        if (!NetworkManager.Singleton.StartHost())
        {
            NetworkManager.Singleton.StartClient();
        }
        else
        {
            if (IsServer)
            {
                foreach (var spawnObject in SpawnableObjects)
                {
                    Instantiate(spawnObject).GetComponent<NetworkObject>().Spawn();
                }
                
            }
        }
    }

}