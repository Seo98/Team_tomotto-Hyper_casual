using UnityEngine;
using System.Collections;

public class Boss_R : Monster
{

    //FSM (두개밖에없긴함)
    private enum BossState { Idle, Attacking }
    private BossState currentState;
    private Coroutine currentAttackCoroutine;

    //카메라 / 스크린
    private Camera mainCamera;
    private Vector2 screenBounds;

    // UI 매니저
    public UIManager uiManager;

    //
    private Animator animator;

    // 공격 세팅
    [Header("총알 프리팹 / 발사 포지션")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Player 타겟")]
    public Transform playerTransform;

    [Header("공격 쿨타임")]
    public float idleTime = 1.0f; // 공격 사이 대기 시간
    public float attackCooldown = 1.5f; // 다른 공격 패턴 사이의 쿨다운

    [Header("총알비 연출 프리팹")]
    public GameObject rainEffectPrefab; // 총알비 패턴 시 생성될 이펙트 프리팹
    public float rainEffectOffscreenOffset = 2f; // 총알비 연출 프리팹이 카메라 밖으로 나갈 오프셋

    //공격 패턴
    [Header("패턴 1 : 소용돌이")]
    public int circularAttackBulletCount = 32;
    public float circularAttackBulletSpeed = 4f;
    public int circularAttackRepeatCount = 3; // 원형 공격 반복 횟수
    public float slowCircularAttackBulletSpeed = 2f; // 느린 원형 공격 총알 속도
    public float circularAttackBulletDelay = 0.05f; // 각 총알 발사 사이의 딜레이
    public float circularAttackRotationPerWave = 15f; // 한 웨이브(전체 원형 공격) 후 회전할 각도

    [Header("패턴 2 : 유도탄 날리기")]
    public int homingBurstCount = 4; // 한 번에 발사할 유도탄 수
    public float homingBurstSpeed = 8f;
    public float timeBetweenHomingShots = 0.2f; // 연사 간격

    [Header("패턴 3 : 개지랄")]
    public int spiralBulletCount = 45;
    public float spiralBulletSpeed = 5f;
    public float spiralBulletDelay = 0.04f;
    public float spiralWobbleFrequency = 4f; // 나선이 흔들리는 빈도
    public float spiralWobbleMagnitude = 12f; // 나선이 흔들리는 강도
    public float spiralAsymmetry = 1.05f; // 두 번째 나선의 비대칭성
    public int spiralAttackRepeatCount = 2; // 이중 나선 공격 반복 횟수
    public float spiralAttackMoveSpeed = 1f; // 이중 나선 공격 시 좌우 이동 속도
    public float spiralAttackMoveRange = 1f; // 이중 나선 공격 시 좌우 이동 범위 (중앙에서 각 방향으로)
    
    [Header("패턴 4 : 메테오 발사후 브레스")]
    public GameObject rainingBulletPrefab;
    public int rainingBulletCount = 20;
    public float rainingBulletSpeed = 6f;
    public float rainSpawnWidth = 10f; //간격
    public float minRainingBulletDelay = 0.08f; // 총알비 최소 딜레이
    public float maxRainingBulletDelay = 0.2f; // 총알비 최대 딜레이
    // 아래부터는 총알
    public int targetedAttackBulletCount = 5; // 타겟에게 쏘는 총알갯수
    public float targetedAttackSpreadAngle = 20f;
    public float targetedAttackBulletSpeed = 7f; // 총알속도
    public float rainingBulletSpawnDelay = 0.1f; // 각 총알 생성 전 고정 딜레이

    [Header("전멸기 시간")]
    public float doomsdayTime = 60f;
    private bool doomsdayActivated = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        BossSetting();
    }

    void BossSetting()
    {
        Debug.Log("보스 세팅완료");

        // 카메라 계산
        mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        screenBounds = new Vector2(cameraWidth / 2, cameraHeight / 2);

        animator = GetComponent<Animator>();

        if (firePoint == null) firePoint = transform;

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
            else
            {
                Debug.LogError("Boss FSM 오류: 플레이어를 찾을 수 없습니다.");
                enabled = false;
                return;
            }
        }

        currentState = BossState.Idle;
        StartCoroutine(BossAI_Routine());
        StartCoroutine(DoomsdayTimer());
    }

    protected override void Initialize()
    {
        hp = 999999f;
    }

    private IEnumerator IdleState()
    {
        

        yield return new WaitForSeconds(idleTime);
        currentState = BossState.Attacking;
    }

    // --- FSM 로직 ---
    private IEnumerator BossAI_Routine()
    {
        while (true)
        {
            switch (currentState)
            {
                case BossState.Idle:
                    yield return StartCoroutine(IdleState());
                    break;
                case BossState.Attacking:
                    yield return StartCoroutine(AttackState());
                    break;
            }
        }
    }

    

    private IEnumerator AttackState()
    {
        int randomAttack = Random.Range(0, 4);
        switch (randomAttack)
        {
            case 0:
                animator.SetBool("isAttack", true);
                currentAttackCoroutine = StartCoroutine(CircularAttackPattern());
                break;
            case 1:
                animator.SetBool("isAttack", true);
                currentAttackCoroutine = StartCoroutine(HomingBurstPattern());
                break;
            case 2:
                animator.SetBool("isAttack", true);
                currentAttackCoroutine = StartCoroutine(DoubleSpiralAttackPattern());
                break;
            case 3:
                animator.SetBool("isAttack", true);
                currentAttackCoroutine = StartCoroutine(CombinationAttackPattern());
                break;
        }

        yield return currentAttackCoroutine;
        yield return new WaitForSeconds(attackCooldown);
        currentState = BossState.Idle;
    }

    

    //공격 패턴 구현
    private IEnumerator CircularAttackPattern()
    {
        Debug.Log("보스: 소용돌이");
        float totalRotation = 0f; // 전체 원형 공격의 시작 각도
        for (int repeat = 0; repeat < circularAttackRepeatCount; repeat++)
        {
            float angleStep = 360f / circularAttackBulletCount;
            for (int i = 0; i < circularAttackBulletCount; i++)
            {
                float angle = i * angleStep + totalRotation;
                FireBullet(AngleToDirection(angle), slowCircularAttackBulletSpeed);
                yield return new WaitForSeconds(circularAttackBulletDelay);
            }
            totalRotation += circularAttackRotationPerWave; // 다음 웨이브를 위해 전체 각도 증가
            yield return new WaitForSeconds(0.2f); // 각 반복 사이의 딜레이
        }
        animator.SetBool("isAttack", false);
    }

    private IEnumerator HomingBurstPattern()
    {
        Debug.Log("보스: 유도탄");
        for (int i = 0; i < homingBurstCount; i++)
        {
            if (playerTransform == null) yield break;
            Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;
            FireBullet(directionToPlayer, homingBurstSpeed);
            yield return new WaitForSeconds(timeBetweenHomingShots); // 어떤건 숫자고 어떤건 영어면 public으로 수치 테스트 하기 위함.
        }
        animator.SetBool("isAttack", false);
    }

    private IEnumerator DoubleSpiralAttackPattern()
    {
        Debug.Log("보스: 발광");
        Vector3 initialPosition = transform.position;
        Coroutine moveCoroutine = StartCoroutine(MoveBossDuringSpiralAttack(initialPosition.x));

        for (int repeat = 0; repeat < spiralAttackRepeatCount; repeat++)
        {
            float angleStep = 1440f / spiralBulletCount; // 4바퀴 회전
            float currentAngle1 = 0f;
            float currentAngle2 = 180f; // 180도 반대에서 시작

            for (int i = 0; i < spiralBulletCount; i++)
            {
                float wobble = Mathf.Sin((float)i / spiralBulletCount * Mathf.PI * 2 * spiralWobbleFrequency) * spiralWobbleMagnitude;
                FireBullet(AngleToDirection(currentAngle1 + wobble), spiralBulletSpeed);
                FireBullet(AngleToDirection(currentAngle2 - wobble), spiralBulletSpeed);
                currentAngle1 += angleStep;
                currentAngle2 -= angleStep * spiralAsymmetry; // 비대칭적으로 회전
                yield return new WaitForSeconds(spiralBulletDelay);
            }
            yield return new WaitForSeconds(0.5f); // 각 반복 사이의 딜레이 (조절 가능)
        }
        StopCoroutine(moveCoroutine);
        transform.position = initialPosition; // 패턴 종료 후 원래 위치로 복귀

        animator.SetBool("isAttack", false);
    }

    private IEnumerator MoveBossDuringSpiralAttack(float startX)
    {
        float targetX = startX + spiralAttackMoveRange;
        bool movingRight = true;

        while (true)
        {
            if (movingRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), spiralAttackMoveSpeed * Time.deltaTime);
                if (transform.position.x >= targetX) movingRight = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(startX - spiralAttackMoveRange, transform.position.y, transform.position.z), spiralAttackMoveSpeed * Time.deltaTime);
                if (transform.position.x <= startX - spiralAttackMoveRange) movingRight = true;
            }
            yield return null;
        }
    }

    private IEnumerator CombinationAttackPattern()
    {
        Debug.Log("보스: 사랑비가내려와");
        GameObject portal = null;
        Transform rainSource;

        if (rainEffectPrefab != null)
        {
            portal = Instantiate(rainEffectPrefab, firePoint.position, Quaternion.identity);
            //
            animator.SetBool("isAttack", false);

            rainSource = portal.transform;
            Vector3 portalTargetPosition = new Vector3(transform.position.x, mainCamera.transform.position.y + screenBounds.y + rainEffectOffscreenOffset, 0);
            float portalMoveSpeed = 8f;

            while (Vector3.Distance(portal.transform.position, portalTargetPosition) > 0.1f)
            {
                portal.transform.position = Vector3.MoveTowards(portal.transform.position, portalTargetPosition, portalMoveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("연출용 프리팹 미할당");
            rainSource = transform;
        }

        StartCoroutine(RainingBulletsFrom(rainSource));

        yield return new WaitForSeconds(1.5f);
        if (playerTransform != null)
        {
            Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;
            float startAngle = -targetedAttackSpreadAngle / 2;
            float angleStep = targetedAttackSpreadAngle / (targetedAttackBulletCount - 1);

            for (int i = 0; i < targetedAttackBulletCount; i++)
            {
                float currentAngle = startAngle + (angleStep * i);
                Vector2 fireDirection = Quaternion.Euler(0, 0, currentAngle) * directionToPlayer;
                FireBullet(fireDirection, targetedAttackBulletSpeed);
            }
        }

        animator.SetBool("isAttack", false);

        yield return new WaitForSeconds(2.5f);
        if (portal != null) Destroy(portal);
    }

    private IEnumerator RainingBulletsFrom(Transform spawnTransform)
    {
        float spawnY = spawnTransform.position.y;
        float minX = mainCamera.transform.position.x - screenBounds.x;
        float maxX = mainCamera.transform.position.x + screenBounds.x;

        for (int i = 0; i < rainingBulletCount; i++)
        {
            float spawnX = Random.Range(minX, maxX);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            FireBulletAt(spawnPosition, Vector2.down, rainingBulletSpeed, rainingBulletPrefab);
            animator.SetBool("isAttack", true);
            yield return new WaitForSeconds(rainingBulletSpawnDelay);
        }
    }

    // 총알 발사 사전 설정 유틸

    private void FireBullet(Vector2 direction, float speed)
    {
        FireBulletAt(firePoint.position, direction, speed, bulletPrefab);
    }

    private void FireBulletAt(Vector2 position, Vector2 direction, float speed, GameObject prefab)
    {
        if (prefab == null) return;
        GameObject bullet = Instantiate(prefab, position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = direction.normalized * speed;
    }

    private Vector2 AngleToDirection(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    // 전멸기 로직

    // DEV_S
    // FIXME : NULL 레퍼런스 이슈, 원인 UI 매니저 관련 추정
    private IEnumerator DoomsdayTimer()
    {
        yield return new WaitForSeconds(doomsdayTime);
        if (!doomsdayActivated)
        {
            ActivateDoomsday();
            doomsdayActivated = true;
        }
    }

    private void ActivateDoomsday()
    {
        Debug.Log("보스: 전멸기 발동!");
        // TODO : 연출 구현해야함 

        uiManager.GameOver();
    }
}