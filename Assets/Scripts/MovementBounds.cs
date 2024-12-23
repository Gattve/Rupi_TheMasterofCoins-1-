using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBounds : MonoBehaviour
{
    [Header("Bounds Settings")]
    [SerializeField] private float minX = -30f;
    [SerializeField] private float maxX = 30f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 4.75f;

    private Transform playerTransform;

    private void Start()
    {
        // Cache the player's Transform component
        playerTransform = transform;
    }

    private void LateUpdate()
    {
        ClampPlayerPosition();
    }

    private void ClampPlayerPosition()
    {
        // Restrict the player's position within the bounds
        float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

        // Apply the clamped position
        playerTransform.position = new Vector3(clampedX, clampedY, playerTransform.position.z);
    }
}

