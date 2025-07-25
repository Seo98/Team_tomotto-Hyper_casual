using UnityEngine;

public class Octopus : Monster
{
    [Header("문어 전용")]
    public GameObject inkPrefab;     // 먹물 투사체 프리팹
    public GameObject FirePos;     // 공격 위치
    public float fireRate = 2f;      // 발사 주기

    [Header("이동 범위")]
    public float rightBoundary = 5f;
    public float leftBoundary = -5f;

    private float nextFireTime = 0f;

    protected override void Initialize()
    {
        hp = 1f;
        speed = 1f;
        dir = Vector3.right; // 초기 방향
    }

    
    void Update()
    {
        MonsterLevelUp();

        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        // 이동
        transform.position += dir * (speed + s_player.c_moveSpeed) * Time.deltaTime;

        // 방향 전환
        if (dir == Vector3.right && transform.position.x >= rightBoundary)
        {
            dir = Vector3.left;
        }
        else if (dir == Vector3.left && transform.position.x <= leftBoundary)
        {
            dir = Vector3.right;
        }
    }

    private void HandleShooting()
    {
        if (Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Fire()
    {
        GameObject target = GameObject.FindWithTag("Player");
        if (target == null || inkPrefab == null || FirePos == null)
        {
            return; // 사전처리
        }

        GameObject tb = Instantiate(inkPrefab);
        tb.transform.position = FirePos.transform.position;
    }
}

