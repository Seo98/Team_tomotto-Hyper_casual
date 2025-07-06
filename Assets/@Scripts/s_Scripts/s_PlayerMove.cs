using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class s_PlayerMove : MonoBehaviour
{
    private Rigidbody2D s_rb;
    private s_PlayerInfo s_playerInfo;
    private s_PlayerInput s_playerInput;

    public void Setup(s_PlayerInfo info, s_PlayerInput input)
    {
        s_playerInfo = info;
        s_playerInput = input;
        s_rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (s_playerInfo == null || s_playerInput == null) return;
        HandleMovement();
    }

    private void HandleMovement()
    {
        // s_PlayerInput으로부터 최종 방향 벡터를 받아와서 속도만 곱해줍니다.
        s_rb.linearVelocity = s_playerInput.s_MoveDirection * s_playerInfo.s_moveSpeed;
    }
}