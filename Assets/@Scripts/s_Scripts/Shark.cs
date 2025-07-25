using UnityEngine;

public class Shark : Monster
{
    private bool isFollowing = false;

    protected override void Initialize()
    {
        // 능력치 설정
        isFollowing = true;
        hp = 2f;
        speed = 1f;
    }

    void Update()
    {
        MonsterLevelUp();

        transform.position += dir * (speed + s_player.c_moveSpeed) * Time.deltaTime;

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
