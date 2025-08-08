using UnityEngine;

public class Shark : Monster
{
    // Dev_H : 상어의 특성 스크립트, 속도와 체력이 높고 플레이어를 따라온다는 특징이 있습니다.

    // Dev_S: 보시면 아시겠지만 is Following < 굳이 안써도 됩니다
    // 상어만의 독립 구역이라 참고하시고 수정하셔도 되염 
    private bool isFollowing = false;

    protected override void Initialize()
    {
        animator = GetComponent<Animator>();

        isFollowing = true;
        hp = 2f;
        speed = 2f;
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
