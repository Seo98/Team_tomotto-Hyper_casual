using UnityEngine;

public class IceAttack : BaseAttack
{

    [Header("ICE 전용")]
    public float slowPercentage = 0.5f;  // 50% 속도 감소
    public float slowDuration = 3f;      // 3초간 지속

    protected override void Initialize()
    {
        attackType = AttackType.ICE;
        damage = 1f;  // 슬로우 효과 있어서 약간 낮게? 흠..
        spawnTime = 3f;  // 유도탄이니까 조금 느리게
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            Attack();
            timer = 0f;
        }
    }
    protected override void Attack()
    {
        Instantiate(AttackManager.Instance.iceProjectilePrefab,AttackManager.Instance.firePositions[3].position,Quaternion.identity);
    }

    public override void Upgrade(float damageIncrease, float spawnSpeedIncrease)
    {
        damage += damageIncrease;
        spawnTime = Mathf.Max(0.1f, spawnTime - spawnSpeedIncrease);
    }
}