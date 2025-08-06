using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // Dev_H : 동일한 태그를 가진 오브젝트를 파괴하는 정리용 스크립트입니다.

    // Dev_S : 시웅님쪽 라인
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Dev_H :  일단 파괴 예상되는 애들 일일히 태그 입력했지만,
        //          파괴 대상 오브젝트들 레이어를 지정해서 레이어간 상호작용 활용할 수 도 있습니다

        // Dev_H_8/6 : string 배열로 인스펙터에서 직접 태그들을 연결하는 방식으로 코드 줄일까 합니다

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

        if (other.CompareTag("EnemyBullet"))
        Destroy(other.gameObject);
    }
}