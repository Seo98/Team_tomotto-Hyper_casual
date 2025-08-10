using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    SoundManager sManager;
    public PlayerController player; // 은주님쪽 라인 참조

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        sManager = FindFirstObjectByType<SoundManager>();

    }

    private void OnTriggerEnter2D(Collider2D other) //복붙 & 트리거로 설정
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.isDamaged) return; // dev_h : 데미지 받고 잠깐 무적시 충돌 방지

            sManager.EventSoundPlay("hitting");
            Destroy(this.gameObject);

            if (player.isShield)
            {
                player.BreakShield();
                return;
            }

            player.StartCoroutine(player.Invincibility()); // dev_h : 데미지 받는 함수 호출
        }
    }
}
