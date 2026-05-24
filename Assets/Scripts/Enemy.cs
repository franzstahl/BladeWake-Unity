using UnityEngine;

public class Enemy : Health
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float distance = 2f;
    [SerializeField] private float detectionRadius = 3.0f;
    [SerializeField] private float separationWeight = 10.0f;
    [SerializeField] private float smoothing = 0.2f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _playerTransform;
    private Vector2 _currentDirection;
    private Vector2 _lastDirection;
    [SerializeField] private AudioClip _attackSound;
    
   

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
        Collider2D[] neighbours = Physics2D.OverlapCircleAll(_rb.position, detectionRadius, enemyLayer);
        MoveTowardsPlayer(neighbours);
    }

    private void MoveTowardsPlayer(Collider2D[] neighbours)
    {
        Vector2 separationForce = Vector2.zero;

        foreach (Collider2D neighbour in neighbours)
        {
            if (neighbour.gameObject != gameObject)
            {
                Vector2 pushDirection = (Vector2)transform.position - (Vector2)neighbour.transform.position;
                separationForce += pushDirection;

            }
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
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            _animator.SetTrigger("Attack");
            _audioSource.PlayOneShot(_attackSound);
        }
    }
}
