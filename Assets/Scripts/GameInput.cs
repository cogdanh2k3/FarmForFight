using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction m_playerInputAction;

    private void Awake()
    {
        m_playerInputAction = new PlayerInputAction();
        m_playerInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = m_playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
