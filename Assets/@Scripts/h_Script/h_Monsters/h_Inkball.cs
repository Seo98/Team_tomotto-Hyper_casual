using UnityEngine;

public class h_Inkball : MonoBehaviour
{
    public float speed = 4;

    // �浹 ȿ�� �ν�����
    // public GameObject explosionFactory;

    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �浹 ȿ��
        // GameObject explosion = Instantiate(explosionFactory);
        // explosion.transform.position = transform.position;

        Destroy(gameObject);

        if (other.gameObject.CompareTag("Player"))
            FindFirstObjectByType<h_InkEffect>().PlayEffect();
    }
}
