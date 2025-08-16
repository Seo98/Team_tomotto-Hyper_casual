using UnityEngine;

public class OrangeOctBullet : MonoBehaviour
{
    SoundManager sManager;
    public PlayerController player; 
    public float speed = 4;
    FeverTimeManager feverTimeManager;


    void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        sManager = FindFirstObjectByType<SoundManager>();
        feverTimeManager = FindFirstObjectByType<FeverTimeManager>();
    }
    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.isDamaged) return;

            Destroy(this.gameObject);
            if (feverTimeManager.isFever == true) return;


            sManager.EventSoundPlay("hitting");
            if (player.isShield)
            {
                player.BreakShield();
                return;
            }

            player.StartCoroutine(player.Invincibility()); 
        }
    }
}