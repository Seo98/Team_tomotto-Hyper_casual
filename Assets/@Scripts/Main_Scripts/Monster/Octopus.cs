using UnityEngine;

public class Octopus : Monster
{
    // Dev_H : 문어의 특성 스크립트, 원거리 공격을 하며 다른 몬스터와 다르게 좌우 왕복 운동을 합니다.
    public GameObject particle;

    // Dev_S: 문어
    [Header("문어 전용")]
    public GameObject inkPrefab;
    public GameObject FirePos;
    public float fireRate = 2f;

    

    [Header("이동 범위")]
    // Dev_H : -8 ~ 8 의 범위는 보기 좋게 뭉쳐다니도록 의도한 범위이니 참고해주세요. (스폰 타임도요)
    public float rightBoundary = 8f;
    public float leftBoundary = -8f;

    private float nextFireTime = 0f;
    protected override void Initialize()
    {
        animator = GetComponent<Animator>();

        hp = 1f;
        speed = 1f;
        dir = Vector3.right;
    }

    // Dev_H: 경험치 부여량
    void Start()
    {
        expAmount = 10;
    }

    void Update()
    {
        MonsterLevelUp();
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        transform.position += dir * (speed + player.moveSpeed) * Time.deltaTime;

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
            return;
        }
        animator.SetTrigger("isAttack");
        GameObject tb = Instantiate(inkPrefab);
        tb.transform.position = FirePos.transform.position;
    }
}

