using UnityEngine;

public class s_PlayerController : MonoBehaviour
{

    [Header("플레이어 능력치")]
    public float c_moveSpeed = 5f;
    public float c_hp = 3f;

    [Header("상태")]
    public bool c_isAttack = false;
    public int c_score = 0;
    public bool c_isShield = false;

    c_BonusItem currState;
    c_FeverTimeManage c_fever;
    ItemManager c_itManage;

    private s_PlayerInfo s_playerInfo; // 플레이어의 정보
    public GameObject c_cannonPrefab;
    public GameObject c_firePosition;

    public float c_spawnTime = 2f;
    float c_timer;

    private Rigidbody2D s_rb;
    private Vector2 s_moveDirection;

    void Awake()
    {
        s_playerInfo = GetComponent<s_PlayerInfo>();
        currState = FindFirstObjectByType<c_BonusItem>();
        s_rb = GetComponent<Rigidbody2D>();
        c_fever = FindFirstObjectByType<c_FeverTimeManage>();
        c_itManage = FindFirstObjectByType<ItemManager>();
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

    private void OnCollisionEnter2D(Collision2D monster)
    {        
        if(c_isShield)
        {
            c_isShield = false;
            monster.gameObject.SetActive(false);
            c_itManage.c_ShiledHeart.SetActive(false);
            c_itManage.c_Shiledimage.SetActive(false);
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

        Vector2 direction = new Vector2(moveHorizontal, 1); // 위로 이동으로 수정(c)
        s_moveDirection = direction.normalized;
    }

    private void HandleMovement()
    {
        s_rb.linearVelocity = s_moveDirection * c_moveSpeed;
    }

    void BreakShield()
    {

    }
}
