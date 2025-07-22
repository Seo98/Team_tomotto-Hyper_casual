using UnityEngine;

public class s_PlayerController : MonoBehaviour
{

    [Header("플레이어 능력치")]
    public float c_moveSpeed = 5f;
    public float c_hp = 3f;
    // B_FIX : Dev_Seo : 캐논볼에 공격력 넣지 말고 여기다 그냥 일괄처리 하시는게 좋을 것 같습니다.
    public float c_Damage = 1f;
    //>>>>

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

            // B_Fix : Dev_Seo
            // >>>>
            //c_timer = 0; 
            //GameObject c_cannonball = Instantiate(c_cannonPrefab, c_firePosition.transform.position, Quaternion.identity);

            // A_Temp_Fix : Dev_Seo
            // 불필요한 변수할당 제거
            // 추후 렉관련 이슈 발생시 오브젝트풀링 작업 필요
            // 해당기능 사용시 피버 매니저 있어야함. 
            c_timer = 0;
            Instantiate(c_cannonPrefab, c_firePosition.transform.position, Quaternion.identity);
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


        // B_Fix : Dev_Seo
        // 추후 터치기반 이동으로 구현해야함. 구현 후 주석처리로 구분사용 가능하게끔 진행요청
        // 좌우 이동시, 카메라가 좌우 이동하지 않도록 수정해야함. >> CameraFollow 스크립트에서 처리필요
        // >>>> 
        //Vector2 direction = new Vector2(moveHorizontal, 1); // 위로 이동으로 수정(c)
        //s_moveDirection = direction.normalized;

        // A_Temp_Fix : Dev_Seo
        Vector2 direction = new Vector2(moveHorizontal, 0); // 수평이동만
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
