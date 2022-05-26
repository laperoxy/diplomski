
using System.Threading;
using UnityEngine;

public class CameraTriggerScript : MonoBehaviour
{
    [SerializeField] private float wantedOrthographicSize = 9.0f;
    [SerializeField] private float zoomIterations = 60;
    [SerializeField] private float zoomSmoothness = 1.1f;
    private bool activated = false;
    private bool shouldUpdate = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!activated)
        {
            shouldUpdate = true;
            activated = true;
        }
    }
    
    private void FixedUpdate()
    {
        if (shouldUpdate)
        {
            float smoothPosition = Mathf.Lerp(Camera.main.orthographicSize, wantedOrthographicSize,
                Time.deltaTime * zoomSmoothness);
            Camera.main.orthographicSize = smoothPosition;
            if (zoomIterations-- <= 0)
            {
                Destroy(this);
            }
        }
    }
}
