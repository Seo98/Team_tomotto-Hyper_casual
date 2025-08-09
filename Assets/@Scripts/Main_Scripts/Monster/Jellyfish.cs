using UnityEngine;
public class Jellyfish : Monster
{
    // Dev_H : 해파리의 특성 스크립트, 따로 특기사항은 없습니다.

    
    protected override void Initialize()
    {
        animator = GetComponent<Animator>();

        hp = 1f;
        speed = 1f;
        dir = Vector3.down;
    }

    // Dev_H: 경험치 부여량
    void Start()
    {
        expAmount = 5;
    }

    void Update()
    {
        MonsterLevelUp();
        transform.position += dir * (speed + player.moveSpeed) * Time.deltaTime; // 아래이동
    }

}
