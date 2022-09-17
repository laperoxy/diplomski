using System;
using Unity.Netcode;
using UnityEngine;

public class EndGameScript: NetworkBehaviour
{
    public static GameObject endGate = null;
    public static bool pvpPhaseOn = false;
    public static void FinishGameIfAllBossesAreDead(GameObject endGate)
    {
        if (EndGameScript.endGate == null)
        {
            EndGameScript.endGate = endGate;
        }

        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (bosses.Length == 1 || bosses.Length == 0)
        {
            pvpPhaseOn = true;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int remainingPlayers = 0;
            foreach (var player in players)
            {
                if (player.transform.position.x > 183)
                {
                    remainingPlayers += 1;
                }
            }
        
            if (remainingPlayers == 1)
            {
                pvpPhaseOn = false;
                Debug.Log("Good game! Congratulations!");
                Instantiate(EndGameScript.endGate).GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}