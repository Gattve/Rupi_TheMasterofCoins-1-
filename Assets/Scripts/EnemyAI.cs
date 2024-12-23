using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Idle,
        Roaming
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private Animator animator; // Add this line
    private float idleTime = 2f; // Time to stay idle
    private float roamTime = 3f; // Time to roam

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        animator = GetComponent<Animator>(); // Initialize the Animator
        state = State.Idle;
    }

    private void Start()
    {
        StartCoroutine(StateRoutine());
    }

    private IEnumerator StateRoutine()
    {
        while (true)
        {
            switch (state)
            {
                case State.Idle:
                    animator.SetBool("isWalking", false); // Set walking animation to false
                    yield return new WaitForSeconds(idleTime);
                    state = State.Roaming;
                    break;

                case State.Roaming:
                    animator.SetBool("isWalking", true); // Set walking animation to true
                    Vector2 roamPosition = GetRoamingPosition();
                    enemyPathfinding.MoveTo(roamPosition);
                    yield return new WaitForSeconds(roamTime);
                    state = State.Idle;
                    break;
            }
        }
    }

    private Vector2 GetRoamingPosition()
    {
        // Generate a random position within a certain range
        return new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)); // Adjust these values based on your map
    }

    
}