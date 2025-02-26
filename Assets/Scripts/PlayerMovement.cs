using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private bool isWalking;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.linearVelocity = Isometry(moveInput) * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context) 
    {
        animator.SetBool("IsWalking", true);
        isWalking = true;

        if (context.canceled)
        {
            animator.SetBool("IsWalking", false);
            isWalking = false;

            animator.SetFloat("LastX", moveInput.x);
            animator.SetFloat("LastY", moveInput.y);
            lastMoveDirection = moveInput;

        }

        moveInput = context.ReadValue<Vector2>();

        animator.SetFloat("X", moveInput.x);
        animator.SetFloat("Y", moveInput.y);
        moveDirection = moveInput;
    }

    private Vector2 Isometry(Vector2 cartesianVector)
    {
        Vector2 isometricVector = new (0,0);

        isometricVector.x = cartesianVector.y + cartesianVector.x;
        isometricVector.y = (-cartesianVector.x + cartesianVector.y)/2;

        return isometricVector;
    }

    public Vector2 GetFacing()
    {
        if (isWalking)
        {
            return Isometry(moveDirection);
        }
        else return Isometry(lastMoveDirection);
    }

    public Vector2 GetUnalteredFacing()
    {
        if (isWalking)
        {
            return moveDirection;
        }
        else return lastMoveDirection;
    }
}
