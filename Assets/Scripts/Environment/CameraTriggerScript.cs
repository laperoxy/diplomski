
using System;
using System.Threading;
using Unity.Netcode;
using UnityEngine;

public class CameraTriggerScript : MonoBehaviour
{

    private const int RECOMMENDED_ZOOM_ITERATIONS = 120;
    [SerializeField] private float wantedOrthographicSize = 9.0f;
    [SerializeField] private float zoomSmoothness = 1.1f;
    [SerializeField] private float ortographicSizeBeforeZoom = 6.11f;
    [SerializeField] private bool shouldFlipCameraZoom = false;
    private Camera mainCamera;
    
    private bool shouldUpdate;
    private int zoomIterations = RECOMMENDED_ZOOM_ITERATIONS;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject followedGameObject = mainCamera.GetComponent<FollowPlayerScript>().followedGameObject;
        if (collision.gameObject == followedGameObject && !isActive())
        {
            --zoomIterations; // to make isActive() function return true
            shouldUpdate = true;
        }
    }

    private void flipWantedOrtographicSize()
    {
        (wantedOrthographicSize, ortographicSizeBeforeZoom) = (ortographicSizeBeforeZoom, wantedOrthographicSize);
    }

    private bool isActive()
    {
        return zoomIterations != RECOMMENDED_ZOOM_ITERATIONS;
    }

    private void resetZoomIterations()
    {
        zoomIterations = RECOMMENDED_ZOOM_ITERATIONS;
    }

    private void FixedUpdate()
    {
        if (shouldUpdate)
        {
            float smoothPosition;
            if (zoomIterations < (RECOMMENDED_ZOOM_ITERATIONS/4))
            {
                smoothPosition = calculateSmoothPositionInLastIterations(zoomIterations);
            }
            else
            {
                smoothPosition = Mathf.Lerp(Camera.main.orthographicSize, wantedOrthographicSize,
                    Time.deltaTime * zoomSmoothness);
            }
            mainCamera.orthographicSize = smoothPosition;
            if (--zoomIterations <= 0)
            {
                mainCamera.orthographicSize = wantedOrthographicSize;
                if (!shouldFlipCameraZoom)
                {
                    Destroy(this);
                }
                else
                {
                    shouldUpdate = false;
                    resetZoomIterations();
                    flipWantedOrtographicSize();
                }
            }
        }
    }

    private float calculateSmoothPositionInLastIterations(int iterations)
    {
        var mainOrthographicSize = mainCamera.orthographicSize;
        float iteration = Math.Abs(wantedOrthographicSize - mainOrthographicSize)/iterations;
        if (wantedOrthographicSize < mainOrthographicSize)
        {
            return mainOrthographicSize - iteration;
        }
        return mainOrthographicSize + iteration;
        
    }
}
