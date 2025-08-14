using System.Collections;
using UnityEngine;

public class FlameProjectile : MonoBehaviour
{
    private FlameAttack flameAttack;
    private Coroutine damageCoroutine;

    private void OnEnable()
    {
        // 브레스가 켜질 때마다 데미지 코루틴 시작
        flameAttack = AttackManager.Instance.GetFlameAttack();

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(ContinuousDamage());
    }

    private void OnDisable()
    {
        // 브레스가 꺼질 때 데미지 코루틴 정지
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator ContinuousDamage()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(flameAttack.damageInterval); // 0.1초 대기

            // 브레스 범위 내 모든 몬스터에게 데미지
            //DealDamageToMonstersInRange();
        }
    }

    /*
    private void DealDamageToMonstersInRange()
    {
        // 브레스 콜라이더 범위 내 몬스터들 찾기
        Collider2D[] hitMonsters = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);

        foreach (Collider2D hit in hitMonsters)
        {
            if (hit.CompareTag("Monster") || hit.CompareTag("Boss"))
            {
                // 몬스터에게 데미지 (Monster.cs의 TakeDamage 호출)
                Monster monster = hit.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.TakeDamage((int)flameAttack.damage);
                }
            }
        }
    }
    */
}
