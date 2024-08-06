using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public Vector3 offset;         // Offset between the camera and player
    public float smoothSpeed = 0.3f; // How smooth the camera movement will be
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate() //bei SmoothDamp interpolate -> none sonst geht nicht
    {
        Vector3 targetPosition = player.position + offset;


        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

    }
}
