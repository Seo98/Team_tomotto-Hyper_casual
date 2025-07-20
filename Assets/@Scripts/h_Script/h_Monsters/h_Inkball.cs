using UnityEngine;

public class h_Inkball : MonoBehaviour
{
    public float speed = 4;

    // 충돌 효과 인스펙터
    // public GameObject explosionFactory;

    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌 효과
        // GameObject explosion = Instantiate(explosionFactory);
        // explosion.transform.position = transform.position;

        Destroy(gameObject);

        if (other.gameObject.CompareTag("Player"))
            FindObjectOfType<h_InkEffect>().PlayEffect();
    }
}
