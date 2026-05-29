using System.Collections;
using UnityEngine;

public class Enemy : Health
{
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float distance = 2.0f;
    [SerializeField] private float detectionRadius = 3.0f;
    [SerializeField] private float separationWeight = 1.5f;
    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float delay = 0.4f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private float damageRange = 1.0f;

    [SerializeField] private bool isBoss;

    private float _lastAttackTime = 0f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _playerTransform;
    private Vector2 _currentDirection;
    private Vector2 _lastDirection;
   
    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();

    }

    private void FixedUpdate() 
    {
        Collider2D[] neighbours = Physics2D.OverlapCircleAll(_rb.position, detectionRadius, enemyLayer); // Get nearby enemies for separation
        MoveTowardsPlayer(neighbours);
    }

    private void MoveTowardsPlayer(Collider2D[] neighbours) // Handle movement towards player and steering flocking behiavour 
    {
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D neighbour in neighbours)
        {
            if (neighbour.gameObject != gameObject)
            {
                Vector2 pushDirection = (Vector2)transform.position - (Vector2)neighbour.transform.position;
                float dist = pushDirection.magnitude;

                if (dist > 0)
                {
                    separationForce += pushDirection.normalized / dist; // Stronger push when closer, weaker when farther
                }

            }
        }

        if (neighbours.Length > 1) 
        {
            separationForce /= (neighbours.Length - 1); 
        }

        float distanceToPlayer = Vector2.Distance(_rb.position, _playerTransform.position); 

        if (distanceToPlayer >= distance)
        {
            Vector2 moveDirection = ((Vector2)_playerTransform.position - _rb.position).normalized;
            Vector2 targetDirection = (moveDirection + (separationForce * separationWeight)).normalized;

            _currentDirection = Vector2.Lerp(_currentDirection, targetDirection, smoothing);

            if (_currentDirection.magnitude > 0.1f)
            {
                _lastDirection = _currentDirection;
            }

            _rb.MovePosition(_rb.position + _currentDirection * speed * Time.fixedDeltaTime);

            _animator.SetFloat("MoveX", _currentDirection.x);
            _animator.SetFloat("MoveY", _currentDirection.y);

        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }

        if (distanceToPlayer <= attackRange)
        {
            TryAttack();
        }

    }

    private void TryAttack()  
    {
        if (Time.time >= _lastAttackTime + attackCooldown)
        {
            _lastAttackTime = Time.time;

            _animator.SetTrigger("Attack");
            _audioSource.PlayOneShot(_attackSound);

            StartCoroutine(DamageDelay());

        }
    }

    private IEnumerator DamageDelay() // Delay the damage application to sync with the attack animation
    {
        yield return new WaitForSeconds(delay);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, damageRange, playerLayer); // Detect player in damage range

        foreach (Collider2D hit in hits)
        {
            Health health = hit.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }

    protected override void Die()
    {
        base.Die();

        if (isBoss) // If this enemy is a boss, notify the wave manager that the boss has died
        {
            WaveManager.instance.BossDied();
        }
        else
        {
            WaveManager.instance.EnemyDied(); // Notify the wave manager that an enemy has died, to manage wave progression
        }
    }






}
    

