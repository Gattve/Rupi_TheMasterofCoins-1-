using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    private Animator animator;

    [SerializeField] public GameObject coinPrefab;
    [SerializeField] public GameObject heartPrefab;

    private void Start() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void ChangeHealth(int amount) {
        currentHealth += amount;

        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0) {
            animator.SetTrigger("isDead");
            StartCoroutine(HandleDeath());
        } else {
            animator.SetTrigger("isHurting");
        }
    }

     private IEnumerator HandleDeath() {
    
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

        DropReward();
    }

    private void DropReward() {

        float dropChance = Random.value;

        if (dropChance < 0.4f) { 
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        } else if (dropChance < 0.8f) { 
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
        
    }
}
