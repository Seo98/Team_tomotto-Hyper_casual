using UnityEngine;

public class Shark : Monster
{
    // Dev_S: 보시면 아시겠지만 is Following < 굳이 안써도 됩니다
    // 상어만의 독립 구역이라 참고하시고 수정하셔도 되염 
    private bool isFollowing = false;

    protected override void Initialize()
    {
        isFollowing = true;
        hp = 2f;
        speed = 2f; // Dev_H : 다소 몬스터의 기능이 부각되는 느낌이 적어, 기존 1이었던 속도를 테스트 삼아 2로 상향 해 보았습니다!
    }

    // Dev_H: 경험치 부여량
    void Start()
    {
        expAmount = 15;
    }

    void Update()
    {
        MonsterLevelUp();

        transform.position += dir * (speed + player.moveSpeed) * Time.deltaTime;

        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {

        GameObject target = GameObject.FindWithTag("Player");
        if (target != null)
        {
            dir = (target.transform.position - transform.position).normalized;
        }
    }
}
