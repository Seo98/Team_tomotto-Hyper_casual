using UnityEngine;

public class BonusItem : MonoBehaviour
{

    // Dev_s : 여기도 한번 주석작업 해주시면.....감사콩 
    // 이쪽라인은 크게 안건드렸던걸로 기억합니다.
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType bonusItem;

    ItemManager itManager;
    PlayerController player;

    SpriteRenderer sr;
    public Cannonball fireball;

    bool isShield;


    public Vector3 dir;
    public float speed = 3;
    GameObject target;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        sr = this.GetComponent<SpriteRenderer>();
        itManager = FindFirstObjectByType<ItemManager>(); 
    }

    private void OnEnable()
    {
        
    }


    public void Update()
    {
        GameObject target = GameObject.FindWithTag("Player");
        transform.position += dir * speed * Time.deltaTime;
        if (target != null)
        {
            dir = (target.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (this.bonusItem == BonusType.SHEILD)
            {
                Shield();
                Destroy(gameObject);
            }
            if(this.bonusItem == BonusType.ATTACKUP)
            {
                AttackUP();
                Destroy(gameObject);
            }
        }
    }

    void Shield()
    {
        Debug.Log("isShield");
        player.isShield = true;
        itManager.ShiledHeart.SetActive(true);
        itManager.Shiledimage.SetActive(true);                     
    }

    void AttackUP()
    {
        fireball.speed = 13f;
        player.spawnTime = 1f;

        player.damage += 1f;
    }
}