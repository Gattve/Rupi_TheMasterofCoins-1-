using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Stat health;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float weaponRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private AttackScript attackScript;

    private float initHealth = 100f;

    private RupiControls rupiControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator rupiAnimator;
    private SpriteRenderer rupiSpriteRenderer;
    private Vector3 localScale;
    private float dirX;
    private bool facingRight = true;

    private AttackBook attackBook;

    private void Awake()
    {
        rupiControls = new RupiControls();
        rb = GetComponent<Rigidbody2D>();
        rupiAnimator = GetComponent<Animator>();
        rupiSpriteRenderer = GetComponent<SpriteRenderer>();
        localScale = transform.localScale;

        attackBook = GetComponent<AttackBook>();
    }

    private void OnEnable()
    {
        rupiControls.Enable();
        SubscribeControls();
    }

    private void OnDisable()
    {
        rupiControls.Disable();
        UnsubscribeControls();
    }

    private void Start()
    {
        health.Initialize(initHealth, initHealth);
    }

    private void Update()
    {
        movement = rupiControls.Movement.Move.ReadValue<Vector2>();
        dirX = movement.x;
        rupiAnimator.SetFloat("moveX", movement.x);
        rupiAnimator.SetFloat("moveY", movement.y);
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }


    private void SubscribeControls()
    {
        rupiControls.Combat.Attack1.started += _ => TryAttack(0);
        rupiControls.Combat.Attack2.started += _ => TryAttack(1);
        rupiControls.Combat.Attack3.started += _ => TryAttack(2);
        rupiControls.Jump.Jump.started += _ => Jump();
        rupiControls.Interact.Interacting.started += _ => Interact();
    }

    private void UnsubscribeControls()
    {
        rupiControls.Combat.Attack1.started -= _ => TryAttack(0);
        rupiControls.Combat.Attack2.started -= _ => TryAttack(1);
        rupiControls.Combat.Attack3.started -= _ => TryAttack(2);
        rupiControls.Jump.Jump.started -= _ => Jump();
        rupiControls.Interact.Interacting.started -= _ => Interact();
    }

    private void Jump()
    {
        rupiAnimator.SetTrigger("Jump");
    }

    private void TryAttack(int attackIndex)
    {
        if (attackBook == null) return;

        Attack attack = attackBook.Attacking(attackIndex);
        if (attack != null && attack.IsReady())
        {
            ExecuteAttack(attack, attackIndex);
        }
        else
        {
            float remainingCooldown = attack.Cooldown - (Time.time - attack.LastUsedTime);
            Debug.Log($"Attack {attack.Name} is on cooldown. Wait {remainingCooldown:F1} seconds.");
        }
    }

    private void ExecuteAttack(Attack attack, int attackIndex)
    {
        attack.Use();
        rupiAnimator.SetTrigger($"Attack{attackIndex + 1}");
        Debug.Log($"Used {attack.Name}");

        // Deteksi musuh dalam jangkauan
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, weaponRange, enemyLayer);
        if (enemy != null)
        {
            enemy.GetComponent<EnemyHealth>()?.ChangeHealth(-attack.Damage);
        }

        // Perbarui UI jika ada
        attackScript?.InitializeAttackUI(attack.Icon, attack.Name, attack.Cooldown);
    }

    public void TakeDamage(float damage)
    {
        GameManager.Instance.PlayerHealth -= (int)damage;
    }

    private void Die()
    {
        Debug.Log("Player died.");
    }

    private void OnDrawGizmosSelected()
    {
        // Untuk debugging jangkauan serangan
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
        }
    }

//     void Interact()
//     {
//         Debug.Log("Interact triggered.");
//         var facingDir = new Vector3(rupiAnimator.GetFloat("moveX"), rupiAnimator.GetFloat("moveY"));
//         var interactPos = transform.position + facingDir;

//         var collider = Physics2D.OverlapCircle(interactPos, 2f, interactableLayer);
//         if (collider != null)
//         {
//             Debug.Log($"Interacting with {collider.name}");
//             Interactable interactable = collider.GetComponent<Interactable>();
//         if (interactable != null)
//         {
//             interactable.Interact();
//         }
//         else
//         {
//             Debug.Log("No Interactable component found.");
//         }
//         }
//         else
//         {
//             Debug.Log("No interactable object found.");
//         }
// }

    void Interact()
    {
        if (gameObject == null)
        {
            Debug.LogWarning("Player object is missing.");
            return;
        }

        var facingDir = new Vector3(rupiAnimator.GetFloat("moveX"), rupiAnimator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
        else
        {
            Debug.Log("No interactable object found.");
        }
    }

}