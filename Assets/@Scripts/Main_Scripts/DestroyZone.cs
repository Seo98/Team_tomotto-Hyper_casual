using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // Dev_S : 시웅님쪽 라인
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fireball"))
        Destroy(other.gameObject);

        if (other.CompareTag("Monster"))
        Destroy(other.gameObject);
    }
}