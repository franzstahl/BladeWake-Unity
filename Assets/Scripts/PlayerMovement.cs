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
        if (playerAttack.isAttacking)
        {
            moveInput = Vector2.zero;
            playerAnimator.SetFloat("Speed", 0f);
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;



        if (moveInput != Vector2.zero)
        {
            lastMoveInput = moveInput;
        }

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);

        
        playerAnimator.SetFloat("LastHorizontal", lastMoveInput.x);
        playerAnimator.SetFloat("LastVertical", lastMoveInput.y);
        
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }

                     

}
