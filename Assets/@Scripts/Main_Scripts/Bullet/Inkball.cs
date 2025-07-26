using UnityEngine;

public class Inkball : MonoBehaviour
{
    // dev_s: 시웅님라인 따로 제가 손본거 없어용 
    public float speed = 4;

    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);

        if (other.gameObject.CompareTag("Player"))
            FindFirstObjectByType<InkEffect>().PlayEffect();
    }
}