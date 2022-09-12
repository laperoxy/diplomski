using System;
using Unity.Netcode;
using UnityEngine;

public class EndGameScript: NetworkBehaviour
{
    public static void FinishGameIfAllBossesAreDead(GameObject endGate)
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (bosses.Length == 1)
        {
            Debug.Log("Good game! Congratulations!");
            Instantiate(endGate).GetComponent<NetworkObject>().Spawn();
        }
    }
}