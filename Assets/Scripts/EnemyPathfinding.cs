using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Move towards the target position smoothly
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 target)
    {
        targetPosition = target; // Set the target position
    }
}