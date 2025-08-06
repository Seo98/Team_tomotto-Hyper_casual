using UnityEngine;

public class Inkball : MonoBehaviour
{
    // Dev_H : 그저 발사되면 속도맞춰 날아가다 플레이어에게 닿으면 InkEffect 발동하는 간단한 기능입니다.

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