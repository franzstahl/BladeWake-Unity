using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private int MaxHealth = 3;
    [SerializeField] private float distance = 1f;

    private int _currentHealth;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _playerTransform;
   

    void Start()
    {
        _currentHealth = MaxHealth;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;


    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = ((Vector2)_playerTransform.position - _rb.position).normalized;

        if (Vector2.Distance(_rb.position, _playerTransform.position) >= distance)
        {
            _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
            _animator.SetFloat("MoveX", direction.x);
            _animator.SetFloat("MoveY", direction.y);

        }

    }
}
