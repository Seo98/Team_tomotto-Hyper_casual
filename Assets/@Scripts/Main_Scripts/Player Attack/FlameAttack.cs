using System.Collections;
using UnityEngine;

public class FlameAttack : BaseAttack
{
    [Header("Flame 전용")]
    public float flameDuration = 2f;      // 브레스 지속시간

    private GameObject flameObject;
    private bool isFlameActive = false;

    protected override void Initialize() // 초기데이터
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
