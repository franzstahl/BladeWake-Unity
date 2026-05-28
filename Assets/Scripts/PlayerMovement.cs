using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D _playerRb;
    private Vector2 _moveInput;
    private Vector2 _lastMoveInput = Vector2.down;
    private Animator _playerAnimator;
    private PlayerAttack _playerAttack;

    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (_playerHealth.isDying) return; // The player cannot move while dying

        if (_playerAttack.isAttacking) // The player cannot move while attacking
        {
            _moveInput = Vector2.zero;
            _playerAnimator.SetFloat("Speed", 0f);
            return;
        }

        // Movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        _moveInput = new Vector2(moveX, moveY).normalized;



        if (_moveInput != Vector2.zero) // Last movement direction for idle animations
        {
            _lastMoveInput = _moveInput;
        }

        // Movement animations
        _playerAnimator.SetFloat("Horizontal", moveX);
        _playerAnimator.SetFloat("Vertical", moveY);
        _playerAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);

        
        _playerAnimator.SetFloat("LastHorizontal", _lastMoveInput.x);
        _playerAnimator.SetFloat("LastVertical", _lastMoveInput.y);
        
    }

    private void FixedUpdate() // Handle physics movement 
    {
        _playerRb.MovePosition(_playerRb.position + _moveInput * speed * Time.fixedDeltaTime);
    }

                     

}
