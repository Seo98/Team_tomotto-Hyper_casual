using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Dev_s : 은주님라인

    [Header("플레이어 능력치")]
    public float moveSpeed = 0.2f;
    public float hp = 3f;
    public float damage = 1f; // < 이 변수가 변경되면 실제 데미지 상승입니다 Canonball 스크립트 변수 damage는 안사용해요!

    [Header("상태")]
    public bool isAttack = false;
    public bool isShield = false;

    // 여기 아래에 있는거 헤더로 묶어서 정리하면 보기 편할거같긴해요

    public int score = 0;

    BonusItem currState;
    public FeverTimeManager fever;
    ItemManager itManage;

    public GameObject cannonPrefab;
    public GameObject firePosition;

    public float spawnTime = 2f;
    float timer;

    private Rigidbody2D rb;

    //
    #region :: 마우스 드래그 관련 변수
    private Camera mainCamera;
    private bool isDragging = false;
    private bool isDirectDrag = false; // 플레이어 위에서 드래그 시작했는지 여부
    private Vector2 targetPosition;
    private float minX, maxX, minY, maxY;
    private Vector3 startPos;
    private bool firstStart;
    #endregion


    void OnEnable()
    {
        // 연결고리
        currState = FindFirstObjectByType<BonusItem>();
        rb = GetComponent<Rigidbody2D>();
        itManage = FindFirstObjectByType<ItemManager>();
        mainCamera = Camera.main;

        // 마우스 드래그 관련
        float camDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, camDistance));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 3f, camDistance));

        if (!firstStart)
        {
            startPos = transform.position;
            firstStart = true;

        } // 맨처음시작인지 여부 확인

        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;

        hp = 3f;
    }


    void Update()
    {
        timer += Time.deltaTime;

        // 마우스 드래그 관련
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

        // 드래그 관련
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

        // Dev_S : 은주님 라인 << 여기서 대포알 생성
        if (timer > spawnTime && !fever.isFever) //2초마다 대포알 생성 
        {
            timer = 0;
            GameObject bullet = Instantiate(cannonPrefab, firePosition.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform;
        }

    }

    void FixedUpdate()
    {
        // 드래그 관련 콜리더 hit가 아닐때 보정 이동합니다.
        if (isDragging)
        {
            if (isDirectDrag)
            {
                rb.MovePosition(targetPosition);
            }
            else
            {
                Vector2 smoothedPosition = Vector2.Lerp(rb.position, targetPosition, 5 * Time.fixedDeltaTime);
                rb.MovePosition(smoothedPosition);
            }
        }
    }

    // dev_s : 실드 깨지는거 함수화 시켯어요 여쪽에다
    public void BreakShield()
    {
        isShield = false;
        itManage.ShiledHeart.SetActive(false);
        itManage.Shiledimage.SetActive(false);
    }
}
