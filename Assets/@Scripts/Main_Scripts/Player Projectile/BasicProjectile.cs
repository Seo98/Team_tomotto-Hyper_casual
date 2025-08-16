using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [Header("기본 설정")]
    public float speed = 10f;
    private Vector3 direction = Vector3.up;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
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
