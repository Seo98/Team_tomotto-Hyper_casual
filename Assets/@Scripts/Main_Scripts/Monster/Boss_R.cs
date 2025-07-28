using UnityEngine;
using System.Collections;

public class Boss_R : Monster
{
    public bool isBoss = false;
    // private float spawnTime = 90f; 
    // 1분 30초
    // 게임시작 후 1분 30초가 지나면 보스 생성(테스트중으로 현재는 미사용입니당)

    // --- FSM 관련 변수 ---
    [Header("총알 / 발사 포지션")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Player 타겟")]
    public Transform playerTransform; // 인스펙터에서 플레이어 트랜스폼 할당 필요함.

    [Header("패턴 1 : 원형공격")]
    public int circularAttackBulletCount = 16; // 원형 공격 시 총알 개수
    public float circularAttackBulletSpeed = 5f; // 스피드

    [Header("패턴 2 : 샷건공격")]
    public int targetedAttackBulletCount = 5; // 타겟 공격 시 총알 개수
    public float targetedAttackSpreadAngle = 20f; // 총알 확산 각도
    public float targetedAttackBulletSpeed = 7f;
    public float timeBetweenShots = 0.1f; // 연사 시 총알 간 지연 시간

    [Header("패턴 3 : 소용돌이공격")]
    public int spiralBulletCount = 30; // 나선형 공격 시 총알 개수
    public float spiralBulletSpeed = 6f;
    public float spiralBulletDelay = 0.05f; // 나선형 총알 발사 간격

    [Header("패턴 4 : 파동 공격")]
    public int waveBulletCount = 7; // 파동 공격 시 한 줄 총알 개수
    public float waveBulletSpeed = 8f;
    public float waveBulletDelay = 0.1f; // 파동 총알 발사 간격
    public float waveSpreadAngle = 45f; // 파동 총알 확산 각도

    [Header("공격 쿨타임")]
    public float idleTime = 2.0f; // 공격 사이 대기 시간
    public float attackCooldown = 3.0f; // 다른 공격 패턴 사이의 쿨다운 시간

    [Header("전멸기 시간")]
    public float doomsdayTime = 60f; // 보스 생성 후 전멸기 발동 시간 (1분)
    private bool doomsdayActivated = false;

    private enum BossState { Idle, Attacking }
    private BossState currentState;
    private Coroutine currentAttackCoroutine;


    protected override void OnEnable()
    {
        base.OnEnable();
        //Invoke("SpawnBoss", spawnTime);
        SpawnBoss();
    }



    void SpawnBoss()
    {
        isBoss = true;
        Debug.Log("보스가 생성되었습니다!");

        // firePoint가 설정되지 않았다면 보스 자신의 트랜스폼 사용
        if (firePoint == null)
        {
            firePoint = transform;
        }

        // 플레이어 트랜스폼이 할당되지 않았다면 자동으로 찾기
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Boss FSM 오류: 플레이어 트랜스폼을 찾거나 할당할 수 없음");
                enabled = false; // 오류 방지를 위해 스크립트 비활성화
                return;
            }
        }

        currentState = BossState.Idle;
        StartCoroutine(BossAI_Routine());

        // 전멸기 타이머 시작
        StartCoroutine(DoomsdayTimer());
    }

    // Dev_S:
    // TODO: 중간보스 로직 

    protected override void Initialize()
    {
        hp = 999999f;
        speed = 1f;
    }

    // --- FSM 로직 ---
    private IEnumerator BossAI_Routine()
    {
        while (true) // 메인 AI 루프
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

    private IEnumerator IdleState()
    {
        yield return new WaitForSeconds(idleTime);
        currentState = BossState.Attacking;
    }

    private IEnumerator AttackState()
    {
        // 4가지 공격 패턴 중 하나를 무작위로 선택
        int randomAttack = Random.Range(0, 4); 

        switch (randomAttack)
        {
            case 0:
                currentAttackCoroutine = StartCoroutine(CircularAttackPattern());
                break;
            case 1:
                currentAttackCoroutine = StartCoroutine(TargetedAttackPattern());
                break;
            case 2:
                currentAttackCoroutine = StartCoroutine(SpiralAttackPattern());
                break;
            case 3:
                currentAttackCoroutine = StartCoroutine(WaveAttackPattern());
                break;
        }

        yield return currentAttackCoroutine; // 현재 공격 코루틴이 끝날 때까지 기다림

        yield return new WaitForSeconds(attackCooldown);
        currentState = BossState.Idle;
    }


    // --- 공격 패턴 ---
    // 패턴 1 : 원형으로 총알을 발사
    private IEnumerator CircularAttackPattern()
    {
        Debug.Log("보스: 원형 공격 실행!");
        float angleStep = 360f / circularAttackBulletCount;
        float currentAngle = 0f;

        for (int i = 0; i < circularAttackBulletCount; i++)
        {
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

            FireBullet(direction, circularAttackBulletSpeed);

            currentAngle += angleStep;
        }

        yield return null; // 한 프레임 내에 패턴 완료
    }

    // 패턴 2 : 원뿔 모양으로 발사
    private IEnumerator TargetedAttackPattern()
    {
        Debug.Log("보스: 샷건 공격 실행!");
        if (playerTransform == null) yield break; // 안전 장치

        Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;

        float startAngle = -targetedAttackSpreadAngle / 2;
        float angleStep = targetedAttackSpreadAngle / (targetedAttackBulletCount - 1);

        for (int i = 0; i < targetedAttackBulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector2 fireDirection = Quaternion.Euler(0, 0, currentAngle) * directionToPlayer;

            FireBullet(fireDirection, targetedAttackBulletSpeed);

            if (timeBetweenShots > 0)
            {
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    // 패턴 3 : 나선형으로 총알을 발사

    private IEnumerator SpiralAttackPattern()
    {
        Debug.Log("보스: 소용돌이 공격 실행!");
        float angleStep = 1080f / spiralBulletCount;
        float currentAngle = 270f;

        for (int i = 0; i < spiralBulletCount; i++)
        {
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

            FireBullet(direction, spiralBulletSpeed);

            currentAngle += angleStep;
            yield return new WaitForSeconds(spiralBulletDelay);
        }
    }

    // 패턴 4 : 파동 형태 발사
    private IEnumerator WaveAttackPattern()
    {
        Debug.Log("보스: 파동 공격 실행!");
        if (playerTransform == null) yield break; // 안전 장치

        Vector2 directionToPlayer = (playerTransform.position - firePoint.position).normalized;
        float baseAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        float startAngle = baseAngle - (waveSpreadAngle / 2);
        float angleStep = waveSpreadAngle / (waveBulletCount - 1);

        for (int i = 0; i < waveBulletCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector2 fireDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            FireBullet(fireDirection, waveBulletSpeed);
        }
        yield return new WaitForSeconds(waveBulletDelay);
    }


    // 총알을 생성하고 발사하는 함수
    private void FireBullet(Vector2 direction, float speed)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("인스펙터에 총알 프리팹이 할당되지 않았습니다!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
        else
        {
            Debug.LogWarning($"총알 프리팹 '{bulletPrefab.name}'에 Rigidbody2D 컴포넌트가 없습니다.");
        }
    }

    // --- 전멸기 로직 ---
    // 임시
    private IEnumerator DoomsdayTimer()
    {
        yield return new WaitForSeconds(doomsdayTime); // 60초

        if (!doomsdayActivated)
        {
            ActivateDoomsday();
            doomsdayActivated = true;
        }
    }

    private void ActivateDoomsday()
    {
        Debug.Log("보스: 전멸기 발동!");
        // TODO : 로직 구현해야함 
    }
}

