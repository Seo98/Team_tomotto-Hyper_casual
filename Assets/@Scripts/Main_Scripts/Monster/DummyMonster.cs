using UnityEngine;

public class DummyMonster : Monster
{
    protected override void Initialize()
    {
        animator = GetComponent<Animator>();

        stageGrowthRate = 1f;
        SetBaseHP(10000f);
        speed = 0f;
        dir = Vector3.down;
    }
    // Dev_H: 경험치 부여량
    void Start()
    {
        expAmount = 5;
    }

    void Update()
    {
        //MonsterLevelUp(); 시간에 따라 성장 -> 스테이지별 성장으로 비활성화 처리 
        transform.position += dir * (speed + player.moveSpeed) * Time.deltaTime; // 아래이동
    }
}
