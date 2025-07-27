using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // Dev_S : 시웅님쪽 라인
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Dev_H :  일단 파괴 예상되는 애들 일일히 태그 입력했지만,
        //          파괴 대상 오브젝트들 레이어를 지정해서 레이어간 상호작용 활용할 수 도 있습니다

        if (other.CompareTag("fireball"))
        Destroy(other.gameObject);

        if (other.CompareTag("Monster"))
        Destroy(other.gameObject);

        if (other.CompareTag("BrokenShip"))
        Destroy(other.gameObject);

        if (other.CompareTag("Rock"))
        Destroy(other.gameObject);

        if (other.CompareTag("Seaweed"))
        Destroy(other.gameObject);

        if (other.CompareTag("Whirlpool"))
        Destroy(other.gameObject);

        if (other.CompareTag("Item"))
        Destroy(other.gameObject);
    }
}