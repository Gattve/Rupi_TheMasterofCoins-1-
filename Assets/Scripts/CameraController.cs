using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private Vector2 minBounds; 
    [SerializeField] private Vector2 maxBounds; 
    [SerializeField] private float smoothSpeed = 0.125f; 

    private float camHalfHeight;
    private float camHalfWidth;

    private void Start()
    {
        // Calculate half dimensions of the camera based on its orthographic size
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Get target position of the camera
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Clamp camera position within the bounds
        float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

        // Smoothly move the camera to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, targetPosition.z), smoothSpeed);
        transform.position = smoothedPosition;
    }
}
