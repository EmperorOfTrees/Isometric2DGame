using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        rb.linearVelocity = Isometry(moveInput) * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private Vector2 Isometry(Vector2 cartesianVector)
    {
        Vector2 isometricVector = new (0,0);

        isometricVector.x = cartesianVector.y + cartesianVector.x;
        isometricVector.y = (-cartesianVector.x + cartesianVector.y)/2;

        return isometricVector;
    }
}
