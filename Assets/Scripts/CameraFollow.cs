using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public Vector3 offset;         // Offset between the cam and player
    public float smoothSpeed = 0.3f; // How smooth the cam movement will be
    private Vector3 velocity = Vector3.zero;

    public Camera cam; // Reference to the Camera
    public float zoomSpeed = 2f; // Speed at which the cam zooms

    public float maxZoomOut = 10f; // Maximum zoom out limit
    public float minZoomIn = 5f; // Minimum zoom in limit (original size)
    private bool isZoomedOut = false; // Track zoom state


    void Update() //bei SmoothDamp interpolate -> none sonst geht nicht
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleZoom();
        }
    }
    void FixedUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }

    void ToggleZoom()
    {
        if (isZoomedOut)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
        isZoomedOut = !isZoomedOut; // Toggle zoom state
    }
    public void ZoomOut()
    {
        cam.orthographicSize = maxZoomOut;
    }
    void ZoomIn()
    {
       
        cam.orthographicSize = minZoomIn;
    }
}
