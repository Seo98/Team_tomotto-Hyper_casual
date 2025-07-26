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
        speed = 1f;
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
