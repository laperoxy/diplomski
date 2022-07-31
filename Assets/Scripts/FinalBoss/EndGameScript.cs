using UnityEngine;

public class EndGameScript
{
    public static void FinishGameIfAllBossesAreDead()
    {
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        if (bosses.Length == 1)
        {
            Debug.Log("Good game! Congratulations!");
        }
    }
}