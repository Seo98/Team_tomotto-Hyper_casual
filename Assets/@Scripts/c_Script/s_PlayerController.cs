using UnityEngine;

public class s_PlayerController : MonoBehaviour
{

    [Header("플레이어 능력치")]
    public float c_moveSpeed = 0.2f;
    public float c_hp = 3f;
    // B_FIX : Dev_Seo : 캐논볼에 공격력 넣지 말고 여기다 그냥 일괄처리 하시는게 좋을 것 같습니다.
    public float c_Damage = 1f;
    //>>>>

    [Header("상태")]
    public bool c_isAttack = false;
    public int c_score = 0;
    public bool c_isShield = false;

    c_BonusItem currState;
    public c_FeverTimeManage c_fever;
    ItemManager c_itManage;

    private s_PlayerInfo s_playerInfo; // 플레이어의 정보
    public GameObject c_cannonPrefab;
    public GameObject c_firePosition;

    public float c_spawnTime = 2f;
    float c_timer;

    private Rigidbody2D s_rb;
    
    private Camera mainCamera;
    private bool isDragging = false;
    private bool isDirectDrag = false; // 플레이어 위에서 드래그 시작했는지 여부
    private Vector2 targetPosition;
    private float minX, maxX, minY, maxY;
    private Vector3 startPos;

    void OnEnable()
    {
        s_playerInfo = GetComponent<s_PlayerInfo>();
        currState = FindFirstObjectByType<c_BonusItem>();
        s_rb = GetComponent<Rigidbody2D>();
        c_itManage = FindFirstObjectByType<ItemManager>();
        mainCamera = Camera.main;

        float camDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, camDistance));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 3f, camDistance));
        startPos = transform.position;

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;

        c_hp = 3f;
    }


    void Update()
    {
        c_timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                isDirectDrag = true;
            }
            else
            {
                isDirectDrag = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            isDragging = true;
            Vector3 screenPos = Input.mousePosition;
            float camDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, camDistance));
            
            float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);
            float clampedY = Mathf.Clamp(worldPos.y, minY, maxY);
            targetPosition = new Vector2(clampedX, clampedY);
        }
        else
        {
            isDragging = false;
            isDirectDrag = false;
        }


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
            GameObject bullet = Instantiate(c_cannonPrefab, c_firePosition.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform;
        }
        
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            if (isDirectDrag)
            {
                s_rb.MovePosition(targetPosition);
            }
            else
            {
                Vector2 smoothedPosition = Vector2.Lerp(s_rb.position, targetPosition, 5 * Time.fixedDeltaTime);
                s_rb.MovePosition(smoothedPosition);
            }
        }
    }

    public void BreakShield()
    {
        c_isShield = false;
        c_itManage.c_ShiledHeart.SetActive(false);
        c_itManage.c_Shiledimage.SetActive(false);
    }
}
