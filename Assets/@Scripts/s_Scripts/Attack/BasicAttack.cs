using UnityEngine;

public class BasicAttack : BaseAttack
{
    [Header("기본공격 초기 세팅값")]
    public int projectileCount = 1;
    public int maxProjectileCount = 3;  // 최대 발사체 개수

    protected override void Initialize()
    {
        attackType = AttackType.BASIC;
        damage = 1f;
        spawnTime = 2f;
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
        if (projectileCount == 1)
        {
            // firePositions(01)
            Instantiate(AttackManager.Instance.basicProjectilePrefab,AttackManager.Instance.firePositions[0].position,Quaternion.identity);
        }
        else if (projectileCount == 2)
        {
            // firePositions(02, 03)
            Instantiate(AttackManager.Instance.basicProjectilePrefab, AttackManager.Instance.firePositions[1].position, Quaternion.identity);
            Instantiate(AttackManager.Instance.basicProjectilePrefab, AttackManager.Instance.firePositions[2].position, Quaternion.identity);
        }
        else if (projectileCount == 3)
        {
            // 모든 포지션
            for (int i = 0; i < 3; i++)
            {
                Instantiate(AttackManager.Instance.basicProjectilePrefab, AttackManager.Instance.firePositions[i].position, Quaternion.identity);
            }
        }
    }

    public override void Upgrade(float damageIncrease, float spawnSpeedIncrease)
    {
        damage += damageIncrease;
        spawnTime = Mathf.Max(0.1f, spawnTime - spawnSpeedIncrease);
    }

    public void UpgradeProjectileCount()
    {
        if (projectileCount < maxProjectileCount)
            projectileCount++;
    }
}