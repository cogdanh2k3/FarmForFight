using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameInput m_gameInput;
    [SerializeField] private float m_moveSpeed = 12f;

    private bool m_isWalking;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = m_gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = m_moveSpeed * Time.deltaTime;
        float playerHeight = 4f;
        float playerRadius = 1.2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // cannot move towards moveDir

            // -> Try move only X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            
            if (canMove)
            {
                // can move only on the x
                moveDir = moveDirX;
            }
            else
            {
                // cannot move only on the x

                // -> try move only z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    // can move only on the z
                    moveDir = moveDirZ;
                }
                else
                {
                    // cannot move in any direction
                }
            }

        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        m_isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return m_isWalking;
    }
}
