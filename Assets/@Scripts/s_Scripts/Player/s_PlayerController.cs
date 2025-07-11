using UnityEngine;

public class s_PlayerController : MonoBehaviour
{
    [SerializeField]
    private s_PlayerInfo s_playerInfo; // 플레이어의 정보

    private Rigidbody2D s_rb;
    private Vector2 s_moveDirection;

    void Awake()
    {
        // 겟 컴포넌트
        s_playerInfo = GetComponent<s_PlayerInfo>();
        s_rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 입력 처리
        HandleInput();
    }

    void FixedUpdate()
    {
        // 움직임 처리
        HandleMovement();
    }

    private void HandleInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 direction = new Vector2(moveHorizontal, -1); // 아래로 이동 포함
        s_moveDirection = direction.normalized;
    }

    private void HandleMovement()
    {
        s_rb.linearVelocity = s_moveDirection * s_playerInfo.s_moveSpeed;
    }
}
