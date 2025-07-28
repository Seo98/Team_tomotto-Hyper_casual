using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 플레이어를 찾지 못했을 경우를 대비하여 자기 자신을 파괴
        // Invoke("SelfDestruct", 5f); 
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            // 플레이어를 찾지 못했다면 그냥 직진
            rb.linearVelocity = transform.up * speed;
            return;
        }

        Vector2 direction = (Vector2)player.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.linearVelocity = transform.up * speed;
    }

    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }

    void SelfDestruct()
    {
        // 만약 플레이어를 찾지 못했다면 5초 후에 스스로 파괴
        if (player == null)
        {
            Destroy(gameObject);
        }
    }
}
