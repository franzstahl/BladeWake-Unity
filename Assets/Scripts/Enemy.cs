using UnityEngine;

public class Enemy : Health
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private float detectionRadius = 2.0f;
    [SerializeField] private float separationWeight = 10.0f;
    [SerializeField] private float smoothing = 0.2f;
    [SerializeField] private LayerMask enemyLayer;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _playerTransform;
    private Vector2 _currentDirection;
   

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

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

        if (Vector2.Distance(_rb.position, _playerTransform.position) >= distance)
        {
            Vector2 moveDirection = ((Vector2)_playerTransform.position - _rb.position).normalized;
            Vector2 targetDirection = (moveDirection + (separationForce * separationWeight)).normalized;

            _currentDirection = Vector2.Lerp(_currentDirection, targetDirection, smoothing);

            _rb.MovePosition(_rb.position + _currentDirection * speed * Time.fixedDeltaTime);
            _animator.SetFloat("MoveX", _currentDirection.x);
            _animator.SetFloat("MoveY", _currentDirection.y);

        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

}
