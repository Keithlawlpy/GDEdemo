using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // The player to follow
    public float smoothSpeed = 0.125f;  // How smooth the camera follows
    public Vector3 offset;  // Offset between camera and player

    private void Start()
    {
        // Set an initial offset based on the player's current position
        offset = new Vector3(0, 3, -10);  // You can adjust this offset
    }

    private void FixedUpdate()
    {
        // Camera follows only vertically (no left-right movement)
        Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);

        // Smooth the movement for smooth following
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update camera position
        transform.position = smoothedPosition;
    }
}
