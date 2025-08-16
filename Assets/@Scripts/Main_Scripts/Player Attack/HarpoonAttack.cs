using UnityEngine;

public class HarpoonAttack : BaseAttack
{
    protected override void Initialize() // 초기데이터
    {
        attackType = AttackType.Harpoon;
        damage = 1.5f;
        spawnTime = 2.5f;
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            SoundManager.Instance.EventSoundPlay("crossBow1");
            Attack();
            timer = 0f;
        }
    }

    protected override void Attack()
    {
        Vector3 spawnPos = AttackManager.Instance.firePositions[4].position;
        Instantiate(AttackManager.Instance.harpoonProjectilePrefab, spawnPos, Quaternion.identity);
    }

    public override void Upgrade(float damageIncrease, float spawnSpeedIncrease)
    {
        Debug.Log("작살공격 획득2");
        isActive = true;
        damage += damageIncrease;
        spawnTime = Mathf.Max(0.1f, spawnTime - spawnSpeedIncrease);
    }
}

