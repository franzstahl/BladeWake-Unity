using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private AudioClip attackSound;

    private Animator playerAnimator;
    private float lastAttackTime = 0f;
    private AudioSource audioSource;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
         if (Input.GetMouseButtonDown(0) && CanAttack())
        {
            Attack();

        }
    }

    private bool CanAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        playerAnimator.SetTrigger("Attack");
        audioSource.PlayOneShot(attackSound);
    }


}
