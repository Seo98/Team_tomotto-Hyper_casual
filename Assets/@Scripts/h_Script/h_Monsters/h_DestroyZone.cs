using UnityEngine;

public class h_DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("fireball"))
        Destroy(other.gameObject);

        if (other.CompareTag("Monster"))
        Destroy(other.gameObject);
    }
}