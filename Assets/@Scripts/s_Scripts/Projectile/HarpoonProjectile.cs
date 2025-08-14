using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HarpoonProjectile : MonoBehaviour
{
    [Header("기본 설정")]
    public float speed = 8f;
    public float lifeTime = 7f; // 7초 후 자동 삭제

    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        // 처음에만 타겟 찾고 방향 설정
        Transform target = FindClosestTarget();

        if (target != null)
        {
            direction = (target.position - transform.position).normalized;

            // 방향에 맞게 회전
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-45);
        }
        else
        {
            direction = Vector3.up;
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
    }

    private void Update()
    {
        // 설정된 방향으로 계속 날아가기
        transform.position += direction * speed * Time.deltaTime;
    }

    private Transform FindClosestTarget()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        Monster[] validMonsters = System.Array.FindAll(monsters, monster => IsInCamera(monster.transform));

        float closestDistance = float.MaxValue;
        Transform closestMonster = null;

        foreach (Monster monster in validMonsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster.transform;
            }
        }

        return closestMonster;
    }

    private bool IsInCamera(Transform target)
    {
        if (target == null) return false;

        Camera cam = Camera.main;
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);

        return screenPos.x >= 0 && screenPos.x <= Screen.width &&
               screenPos.y >= 0 && screenPos.y <= Screen.height;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // 관통형이라 파괴되지 않음
        // 그냥 데미지만 주고 계속 날아감
        if (other.transform.CompareTag("Monster") || other.transform.CompareTag("Boss"))
        {
            // 몬스터는 충돌 처리에서 알아서 데미지 받음
            // 여기서는 아무것도 안 함 (관통)
        }
    }
}
