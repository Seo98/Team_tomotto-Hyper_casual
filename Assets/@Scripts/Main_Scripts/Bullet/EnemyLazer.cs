using UnityEngine;

public class EnemyLazer : MonoBehaviour
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
            sManager.EventSoundPlay("hitting");
            if (player.isShield)
            {
                player.BreakShield();
                return;
            }
            player.hp -= 1f;
        }
    }
}
