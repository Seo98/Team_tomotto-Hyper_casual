using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss_R : Monster
{

    //FSM (두개밖에없긴함)
    private enum BossState { Idle, Attacking }
    private BossState currentState;
    private Coroutine currentAttackCoroutine;

    //카메라 / 스크린/ Hp 바
    private Camera mainCamera;
    private Vector2 screenBounds;


    // UI 매니저
    public UIManager uiManager;

    //Hpbar 
    public Image Hpbar;    

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
    public float slowCircularAttackBulletSpeed = 10f; // 느린 원형 공격 총알 속도
    public float circularAttackBulletDelay = 0.05f; // 각 총알 발사 사이의 딜레이
    public float circularAttackRotationPerWave = 15f; // 한 웨이브(전체 원형 공격) 후 회전할 각도

    [Header("패턴 2 : 유도탄 날리기")]
    public int homingBurstCount = 4; // 한 번에 발사할 유도탄 수
    public float homingBurstSpeed = 8f;
    public float timeBetweenHomingShots = 0.2f; // 연사 간격

    [Header("패턴 3 : 발광")]
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


    [Header("패턴 5 : 레이저 브레스")]
    public GameObject warringBreathPrefab;
    public GameObject breathPrefab;

    public GameObject warringBreathPrefab2;
    public GameObject breathPrefab2;
    public int secondBreathLaserCount = 3; // 2단계 브레스 레이저 수


    // 별개 패턴으로 구분
    [Header("패턴 6 : 꽃잎 산개")]
    public int petalWaveCount = 8;           // 꽃잎 개수
    public float petalBulletSpeed = 6f;      // 꽃잎 탄환 속도
    public int petalBulletsPerWave = 5;      // 꽃잎당 탄환 수
    public float petalWaveDelay = 0.1f;      // 꽃잎 간 딜레이
    public float petalSpreadAngle = 15f;     // 꽃잎 내 확산각

    [Header("패턴 7 : 회전 크로스")]
    public float crossRotationSpeed = 45f;   // 크로스 회전 속도
    public int crossBulletCount = 4;         // 십자 방향 수
    public float crossBulletSpeed = 7f;      // 크로스 탄환 속도
    public int crossWaveCount = 15;          // 크로스 발사 횟수
    public float crossWaveDelay = 0.15f;     // 크로스 웨이브 간격

    [Header("패턴 8 : 파동 확산")]
    public int waveRingCount = 6;            // 파동 링 개수
    public int waveBulletsPerRing = 20;      // 링당 탄환 수
    public float waveSpeed = 5f;             // 파동 속도
    public float waveRingDelay = 0.3f;       // 링 간 딜레이
    public float waveSpeedVariation = 2f;    // 속도 변화량


    [Header("패턴 그룹 쿨타임 설정")]
    public float groupACooldown = 1.5f; // 기존 패턴(0-4) 쿨타임
    public float groupBCooldown = 2.0f; // 새 패턴(5-7) 쿨타임

    private bool isGroupAOnCooldown = false;
    private bool isGroupBOnCooldown = false;
    private Coroutine groupACoroutine;
    private Coroutine groupBCoroutine;



    // Dev_S : 354 주석설명 되어있음
    /*
    [Header("전멸기 시간")]
    public float doomsdayTime = 60f;
    private bool doomsdayActivated = false;
    */

    protected override void OnEnable()
    {
        base.OnEnable();     

    }

    public void BossSetting()
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
        // StartCoroutine(DoomsdayTimer());
        // Dev_S : 354 주석설명 되어있음
    }

    protected override void Initialize()
    {
        stageGrowthRate *= 3f;
        SetBaseHP(300f);
        UpdateBar();
    }

    private IEnumerator IdleState()
    {
        yield return new WaitForSeconds(idleTime);
        currentState = BossState.Attacking;
    }

    // --- FSM 로직 ---
    private IEnumerator BossAI_Routine()
    {
        // 두 그룹을 별도로 실행
        StartCoroutine(GroupAAttackRoutine());
        StartCoroutine(GroupBAttackRoutine());

        // 기존 Idle/Attack 루프는 유지하되 빈 루프로 변경
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }


    private IEnumerator GroupAAttackRoutine()
    {
        yield return new WaitForSeconds(idleTime); // 초기 대기

        while (true)
        {
            // 공격 실행
            int randomAttack = Random.Range(0, 5); // 0,1,2,3,4

            switch (randomAttack)
            {
                case 0:
                    animator.SetBool("isAttack", true);
                    groupACoroutine = StartCoroutine(CircularAttackPattern());
                    break;
                case 1:
                    animator.SetBool("isAttack", true);
                    groupACoroutine = StartCoroutine(HomingBurstPattern());
                    break;
                case 2:
                    animator.SetBool("isAttack", true);
                    groupACoroutine = StartCoroutine(DoubleSpiralAttackPattern());
                    break;
                case 3:
                    animator.SetBool("isAttack", true);
                    groupACoroutine = StartCoroutine(CombinationAttackPattern());
                    break;
                case 4:
                    animator.SetBool("isAttack", true);
                    groupACoroutine = StartCoroutine(BreathAttackPattern());
                    break;
            }

            // 공격 완료까지 대기
            yield return groupACoroutine;

            // 공격 후 휴식 시간 (Idle 시간)
            yield return new WaitForSeconds(idleTime);

            // 그룹 A 쿨타임
            yield return new WaitForSeconds(groupACooldown);
        }
    }

    private IEnumerator GroupBAttackRoutine()
    {
        yield return new WaitForSeconds(idleTime + 2f); // 초기 대기 + 더 긴 오프셋

        while (true)
        {
            // 공격 실행
            int randomAttack = Random.Range(5, 8); // 5,6,7

            switch (randomAttack)
            {
                case 5:
                    groupBCoroutine = StartCoroutine(PetalBloomPattern());
                    break;
                case 6:
                    groupBCoroutine = StartCoroutine(RotatingCrossPattern());
                    break;
                case 7:
                    groupBCoroutine = StartCoroutine(WaveExpansionPattern());
                    break;
            }

            // 공격 완료까지 대기
            yield return groupBCoroutine;

            // 공격 후 휴식 시간 (Idle 시간의 절반 정도)
            yield return new WaitForSeconds(idleTime * 0.7f);

            // 그룹 B 쿨타임
            yield return new WaitForSeconds(groupBCooldown);
        }
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
            yield return new WaitForSeconds(0.05f); // 각 반복 사이의 딜레이
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


    private IEnumerator BreathAttackPattern()
    {
        Debug.Log("보스: 브레스");
        Animator breathAnimator = breathPrefab.GetComponent<Animator>();

        warringBreathPrefab.SetActive(true);
        Debug.Log("워링 활성화");
        yield return new WaitForSeconds(1f); // 1초 대기
        Debug.Log("워링 비활성화");
        warringBreathPrefab.SetActive(false);
        SoundManager.Instance.EventSoundPlay("dragon1");
        breathPrefab.SetActive(true);
        Debug.Log("브레스 활성화");

        // 브레스 주 공격 부분 (예: 2초)
        yield return new WaitForSeconds(2f); 

        // 종료 애니메이션 트리거 및 대기
        if (breathAnimator != null)
        {
            Debug.Log("브레스 종료 애니메이션 트리거");
            breathAnimator.SetTrigger("isEnd");

            // 애니메이션 상태가 전환될 시간을 줌
            yield return null; 

            // 전환이 끝날 때까지 대기
            yield return new WaitUntil(() => !breathAnimator.IsInTransition(0));

            Debug.Log("브레스 종료 애니메이션 재생 중");
            // 현재 재생중인 애니메이션(종료 애니메이션)이 끝날 때까지 대기
            yield return new WaitUntil(() => breathAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            Debug.Log("브레스 종료 애니메이션 완료");
        }

        breathPrefab.SetActive(false);
        Debug.Log("브레스 비활성화");

        // 2단계 브레스 패턴 호출
        StartCoroutine(SecondBreathAttackPattern());
        animator.SetBool("isAttack", false); // 일단 혹시모르니박아놔
    }

    private IEnumerator SecondBreathAttackPattern()
    {
        Debug.Log("보스: 2단계 브레스");

        for (int i = 0; i < secondBreathLaserCount; i++)
        {
            // 화면 하단 1/3 내에서 랜덤 위치 계산
            float randomX = Random.Range(-screenBounds.x, screenBounds.x);
            float randomY = Random.Range(-screenBounds.y, -screenBounds.y / 3);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // 랜덤 각도 계산 (수평, 수직, 대각선)
            float[] angles = { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f };
            float randomAngle = angles[Random.Range(0, angles.Length)];
            Quaternion spawnRotation = Quaternion.Euler(0, 0, randomAngle);

            // 경고 프리팹 생성 및 1.5초 대기
            GameObject warningLaser = Instantiate(warringBreathPrefab2, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(1.5f);
            Destroy(warningLaser);

            // 레이저 프리팹 생성
            GameObject laser = Instantiate(breathPrefab2, spawnPosition, spawnRotation);
            
            // 레이저 애니메이션이 끝날 때까지 대기 (Animator가 있다고 가정)
            Animator laserAnimator = laser.GetComponent<Animator>();
            if (laserAnimator != null)
            {
                yield return new WaitUntil(() => laserAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            }
            else
            {
                // 애니메이터가 없으면 1초 후 파괴
                yield return new WaitForSeconds(1f);
            }
            Destroy(laser);
        }

        animator.SetBool("isAttack", false);
    }

    // 패턴 6: 꽃잎 산개 - 꽃잎 모양으로 확산하며 색깔이 변하는 듯한 효과
    private IEnumerator PetalBloomPattern()
    {
        Debug.Log("보스: 꽃잎 산개");

        for (int wave = 0; wave < 3; wave++) // 3번 반복
        {
            float baseAngle = wave * 22.5f; // 각 웨이브마다 회전

            for (int petal = 0; petal < petalWaveCount; petal++)
            {
                float petalAngle = (360f / petalWaveCount) * petal + baseAngle;

                // 각 꽃잎마다 여러 탄환 발사
                for (int bullet = 0; bullet < petalBulletsPerWave; bullet++)
                {
                    float spreadOffset = (bullet - (petalBulletsPerWave - 1) * 0.5f) * (petalSpreadAngle / petalBulletsPerWave);
                    float finalAngle = petalAngle + spreadOffset;

                    Vector2 direction = AngleToDirection(finalAngle);

                    // 거리별로 속도 조절 (안쪽은 느리게, 바깥쪽은 빠르게)
                    float speedMultiplier = 0.7f + (bullet * 0.15f);
                    FireBullet(direction, petalBulletSpeed * speedMultiplier);
                }

                yield return new WaitForSeconds(petalWaveDelay);
            }

            yield return new WaitForSeconds(0.8f); // 웨이브 간 대기
        }

        animator.SetBool("isAttack", false);
    }

    // 패턴 7: 회전 크로스 - 십자 형태로 발사하며 계속 회전
    private IEnumerator RotatingCrossPattern()
    {
        Debug.Log("보스: 회전 크로스");

        float currentRotation = 0f;

        for (int wave = 0; wave < crossWaveCount; wave++)
        {
            // 십자 방향으로 발사 (0, 90, 180, 270도)
            for (int cross = 0; cross < crossBulletCount; cross++)
            {
                float angle = (cross * 90f) + currentRotation;
                Vector2 direction = AngleToDirection(angle);

                // 3연발로 발사하여 더 화려하게
                for (int burst = 0; burst < 3; burst++)
                {
                    FireBullet(direction, crossBulletSpeed);
                    yield return new WaitForSeconds(0.05f);
                }
            }

            // 대각선 방향도 추가 (더 화려함)
            for (int diagonal = 0; diagonal < crossBulletCount; diagonal++)
            {
                float angle = (diagonal * 90f) + 45f + currentRotation;
                Vector2 direction = AngleToDirection(angle);
                FireBullet(direction, crossBulletSpeed * 0.8f); // 대각선은 조금 느리게
            }

            currentRotation += crossRotationSpeed * crossWaveDelay;
            yield return new WaitForSeconds(crossWaveDelay);
        }

        animator.SetBool("isAttack", false);
    }

    // 패턴 8: 파동 확산 - 동심원으로 퍼져나가는 파동
    private IEnumerator WaveExpansionPattern()
    {
        Debug.Log("보스: 파동 확산");

        float ringRotationOffset = 0f;

        for (int ring = 0; ring < waveRingCount; ring++)
        {
            float angleStep = 360f / waveBulletsPerRing;

            for (int bullet = 0; bullet < waveBulletsPerRing; bullet++)
            {
                float angle = bullet * angleStep + ringRotationOffset;
                Vector2 direction = AngleToDirection(angle);

                // 각 링마다 속도 변화 (파동 효과)
                float speedVariation = Mathf.Sin(ring * 0.5f) * waveSpeedVariation;
                float finalSpeed = waveSpeed + speedVariation;

                // 탄환을 약간 뒤쪽에서 시작하여 파동 효과 연출
                Vector2 startPos = (Vector2)firePoint.position + direction * (ring * 0.3f);
                FireBulletAt(startPos, direction, finalSpeed, bulletPrefab);
            }

            // 다음 링은 약간 회전시켜서 더 화려하게
            ringRotationOffset += 15f;
            yield return new WaitForSeconds(waveRingDelay);
        }

        // 마지막에 중앙에서 폭발하는 듯한 효과
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 32; i++)
        {
            float angle = i * (360f / 32f);
            Vector2 direction = AngleToDirection(angle);
            FireBullet(direction, waveSpeed * 1.5f);
        }

        animator.SetBool("isAttack", false);
    }

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

    protected override void Dead()
    {

        StopAllCoroutines();
        // 기존 사망 처리
        base.Dead();

        // 스테이지 클리어 처리
        UIManager uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.GameClear();
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        currHp = Mathf.Clamp(currHp, 0, hp);
        UpdateBar();
    }

    public void UpdateBar()
    {        
        Hpbar.fillAmount = currHp / hp;

    }
}