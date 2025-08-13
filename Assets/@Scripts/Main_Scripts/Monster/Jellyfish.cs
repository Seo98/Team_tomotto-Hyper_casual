using UnityEngine;
public class Jellyfish : Monster
{
    // Dev_H : 해파리의 특성 스크립트, 따로 특기사항은 없습니다.

    
    protected override void Initialize()
    {
        animator = GetComponent<Animator>();

        stageGrowthRate = 1f;
        SetBaseHP(2f);
        speed = 1f;
        dir = Vector3.down;
    }

    // Dev_H: 경험치 부여량
    void Start()
    {
        //damageText.text = ""; //체력바 안 닳을 때
        expAmount = 5;
    }

    void Update()
    {
        //MonsterLevelUp(); 시간에 따라 성장 -> 스테이지별 성장으로 비활성화 처리 
        transform.position += dir * (speed + player.moveSpeed) * Time.deltaTime; // 아래이동
    }

}
