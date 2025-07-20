using UnityEngine;

public class s_PlayerController : MonoBehaviour
{    

    c_FeverTimeManage c_fever;
    private s_PlayerInfo s_playerInfo; // 플레이어의 정보
    public GameObject c_cannonPrefab;
    public GameObject c_firePosition;

    public float c_spawnTime = 2f;
    float c_timer;

    private Rigidbody2D s_rb;
    private Vector2 s_moveDirection;

    void Awake()
    {
        // 겟 컴포넌트
        s_playerInfo = GetComponent<s_PlayerInfo>();
        s_rb = GetComponent<Rigidbody2D>();
        c_fever = FindFirstObjectByType<c_FeverTimeManage>();
    }

    void Update()
    {
        c_timer += Time.deltaTime;
        // 입력 처리
        HandleInput();
        if(c_timer > c_spawnTime && !c_fever.c_isFever) //2초마다 대포알 생성
        {
            c_timer = 0; 
            GameObject c_cannonball = Instantiate(c_cannonPrefab, c_firePosition.transform.position, Quaternion.identity);
        }        
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
