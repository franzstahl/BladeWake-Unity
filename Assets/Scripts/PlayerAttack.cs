using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip attackSound;

    private Animator playerAnimator;
    private float lastAttackTime = 0f;
    private AudioSource audioSource;

    public bool isAttacking = false;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
         if (Input.GetMouseButtonDown(0) && CanAttack()) // Uses left mouse button to attack
        {
            Attack();

        }
    }

    private bool CanAttack() // Check if player can attack based on cooldown
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void Attack() // Handle all the attack logic
    {
        isAttacking = true;
        lastAttackTime = Time.time;
        playerAnimator.SetTrigger("Attack");
        audioSource.PlayOneShot(attackSound);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer); // Detect enemies in range

        foreach (Collider2D hit in hits)
        {
            Health health = hit.GetComponent<Health>();

            if (health != null)
            {
                health.TakeDamage(1);
            }
        }

        Invoke("StopAttacking", attackCooldown);
    }

    private void StopAttacking() // Reset attacking state after cooldown
    {
        isAttacking = false;
    }
}
