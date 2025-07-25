using UnityEngine;


public class Jellyfish : Monster
{
    protected override void Initialize()
    {
        hp = 1f;
        speed = 1f;

        // 이동 방향 설정 (아래)
        dir = Vector3.down;
    }
    void Update()
    {
        MonsterLevelUp();
        transform.position += dir * (speed + s_player.c_moveSpeed) * Time.deltaTime;
    }

}
