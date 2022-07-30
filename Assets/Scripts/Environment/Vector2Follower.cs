using System;
using UnityEngine;

public class Vector2Follower : MonoBehaviour
{

    [SerializeField] private Vector2[] waypoints;

    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex], transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.Lerp(transform.position, waypoints[currentWaypointIndex],
            Time.deltaTime * speed);
    }
}
