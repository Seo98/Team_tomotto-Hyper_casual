using UnityEngine;
public class Jellyfish : Monster
{
    protected override void Initialize()
    {
        hp = 1f;
        speed = 1f;

        dir = Vector3.down;
    }
    void Update()
    {
        MonsterLevelUp();
        transform.position += dir * (speed + player.moveSpeed) * Time.deltaTime; // 아래이동
    }

}
