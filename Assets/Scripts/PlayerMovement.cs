using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Vector2 lastMoveInput = Vector2.down;
    private Animator playerAnimator;
    private PlayerAttack playerAttack;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        
    }

    private void Update()
    {
        if (playerAttack.isAttacking) // The player cannot move while attacking
        {
            moveInput = Vector2.zero;
            playerAnimator.SetFloat("Speed", 0f);
            return;
        }

        // Movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;



        if (moveInput != Vector2.zero) // Last movement direction for idle animations
        {
            lastMoveInput = moveInput;
        }

        // Movement animations
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

        
        playerAnimator.SetFloat("LastHorizontal", lastMoveInput.x);
        playerAnimator.SetFloat("LastVertical", lastMoveInput.y);
        
    }

    private void FixedUpdate() // Handle physics movement 
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }

                     

}
