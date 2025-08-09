using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Dev_s : 은주님라인

    [Header("플레이어 능력치")]
    public float moveSpeed = 0.2f;
    public float hp = 3f;
    public float damage = 1f; // < 이 변수가 변경되면 실제 데미지 상승입니다 Canonball 스크립트 변수 damage는 안사용해요!
    public int score = 0;

    [Header("상태")]
    public bool isAttack = false;
    public bool isShield = false;
    public bool isDamaged = false;

    // 여기 아래에 있는거 헤더로 묶어서 정리하면 보기 편할거같긴해요
  
    [Header("프리팹")]
    public GameObject cannonPrefab;
    public GameObject firePosition;
    public GameObject feverFirePosition_1;
    public GameObject feverFirePosition_2;


    float timer;
    public float spawnTime = 2f;

    //BonusItem bonusIt;
    ItemManager itManage;
    public FeverTimeManager fever;
    private Rigidbody2D rb;

    //Dev_s 
    private BossSpawner boss; // 진짜 큰일났다 스파게티가 되가고있어요

    //Dev_H
    private SpriteRenderer sr;
    
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
        //bonusIt = FindFirstObjectByType<BonusItem>();
        rb = GetComponent<Rigidbody2D>();
        itManage = FindFirstObjectByType<ItemManager>();
        mainCamera = Camera.main;
        boss = GameObject.FindFirstObjectByType<BossSpawner>();

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

        // dev_h : 재시작 할때 투명할 때 가 있어서 추가했습니다
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        Color c = sr.color;
        c.a = 1f;
        sr.color = c;
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

        #region 대포알 생성
        if (timer > spawnTime && !fever.isFever) //피버 타임 아닐 때만 2초마다 대포알 생성 
        {
            timer = 0;
            GameObject bullet = Instantiate(cannonPrefab, firePosition.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform;
        }
        #endregion
        
        // Dev_S : 여기서 피버타임 2일경우 조건에 따라 2개 발사
        if (timer > spawnTime && fever.isFever && boss.isBoss)
        {
            timer = 0;
            GameObject bullet = Instantiate(cannonPrefab, feverFirePosition_1.transform.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(cannonPrefab, feverFirePosition_2.transform.position, Quaternion.identity);
            bullet.transform.parent = this.transform;
            bullet2.transform.parent = this.transform;
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

    // dev_s : 실드 깨지는거 함수화
    public void BreakShield()
    {
        isShield = false;
        itManage.ShiledHeart.SetActive(false);
        itManage.Shiledimage.SetActive(false);
    }

    // dev_h : 체력 피해 입을시 함수
    public IEnumerator Invincibility()
    {
        if (!isDamaged)
        {
            isDamaged = true;

            hp -= 1f;

            // dev_h : 기존 레이어 저장 (피해 입으면 잠깐동안 충돌 막으려고)
            int originalLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("Invincible"); // dev_h : Invincible은 다른 레이어와 충돌하지 않음

            // dev_h : 스프라이트 렌더러 가져오기
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            // dev_h : 3초 동안 무적, 깜빡임 간격
            float duration = 3f;
            float blinkInterval = 0.1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                // 깜빡임: 투명 <-> 원래 색 반복
                sr.enabled = !sr.enabled;

                yield return new WaitForSeconds(blinkInterval);
                elapsed += blinkInterval;
            }

            // 원래 상태 복원
            sr.enabled = true;
            gameObject.layer = originalLayer;
            isDamaged = false;
        }
    }
}
