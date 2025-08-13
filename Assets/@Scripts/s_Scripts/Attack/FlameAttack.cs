using System.Collections;
using UnityEngine;

public class FlameAttack : BaseAttack
{
    [Header("Flame 전용")]
    public float flameDuration = 2f;      // 브레스 지속시간
    public float damageInterval = 0.1f;   // 데미지 판정 간격

    private GameObject flameObject;       // 플레이어에 붙어있는 브레스 오브젝트
    private bool isFlameActive = false;

    protected override void Initialize()
    {
        attackType = AttackType.FLAME;
        damage = 0.5f;
        spawnTime = 3f;

        flameObject = AttackManager.Instance.firePositions[6].gameObject;
        flameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isActive)
        {
            if (flameObject != null && flameObject.activeSelf)
            {
                flameObject.SetActive(false);
                isFlameActive = false;
            }
            return;
        }

        timer += Time.deltaTime;
        if (timer >= spawnTime && !isFlameActive)
        {
            Attack();
            timer = 0f;
        }
    }

    protected override void Attack()
    {
        if (flameObject != null)
        {
            flameObject.SetActive(true);
            isFlameActive = true;

            // flameDuration 후에 끄기
            StartCoroutine(DeactivateFlameAfterDelay());
        }
    }

    private IEnumerator DeactivateFlameAfterDelay()
    {
        yield return new WaitForSeconds(flameDuration);

        if (flameObject != null)
        {
            flameObject.SetActive(false);
        }
        isFlameActive = false;
    }

    public override void Upgrade(float damageIncrease, float spawnSpeedIncrease)
    {
        damage += damageIncrease;
        spawnTime = Mathf.Max(0.1f, spawnTime - spawnSpeedIncrease);
        flameDuration += 0.5f;
    }
}
