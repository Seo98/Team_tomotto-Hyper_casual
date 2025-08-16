using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    [Header("기본 설정")]
    public float speed = 15f;

    private Transform target;
    private float retargetTimer = 0f;
    private float retargetInterval = 0.2f;

    private void Start()
    {
        FindTarget();
    }

    private void Update()
    {
        retargetTimer += Time.deltaTime;

        if (retargetTimer >= retargetInterval)
        {
            FindTarget();
            retargetTimer = 0f;
        }

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // 스프라이트를 타겟 방향으로 회전
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // 위로 직진
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }

    private void FindTarget()
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
        target = closestMonster;
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
        // 몬스터나 보스와 충돌시 파괴
        if (other.transform.CompareTag("Monster") || other.transform.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}