using UnityEngine;

public class BonusItem : MonoBehaviour
{
    
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType bonusItem;

    ItemManager itManager;
    PlayerController player;

    SpriteRenderer sr;

    public Vector3 dir;
    public float speed = 3;
    GameObject target;

    //스크립트 연결
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        sr = this.GetComponent<SpriteRenderer>();
        itManager = FindFirstObjectByType<ItemManager>(); 
    }

    //드랍 아이템 플레이어 방향으로 따라가도록
    public void Update()
    {
        GameObject target = GameObject.FindWithTag("Player");
        transform.position += dir * speed * Time.deltaTime;
        if (target != null)
        {
            dir = (target.transform.position - transform.position).normalized;
        }
    }

    //플레이어가 아이템 먹었을 때
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
        AttackManager attackManager = AttackManager.Instance;
        // 요거 걍 세팅해놓을테니까 생각에 따라 구현하시면 될듯합니다.


        if (attackManager.GetBasicAttack().isActive)
        {
            float currentSpawn = attackManager.GetBasicAttack().spawnTime;
            attackManager.GetBasicAttack().Upgrade(0.2f, currentSpawn * 0f);
        }

        if (attackManager.GetIceAttack().isActive)
        {
            float currentSpawn = attackManager.GetIceAttack().spawnTime;
            attackManager.GetIceAttack().Upgrade(0.2f, currentSpawn * 0f);
        }

        if (attackManager.HarpoonAttack().isActive)
        {
            float currentSpawn = attackManager.HarpoonAttack().spawnTime;
            attackManager.HarpoonAttack().Upgrade(0.2f, currentSpawn * 0f);
        }

        if (attackManager.GetPetAttack().isActive)
        {
            float currentSpawn = attackManager.GetPetAttack().spawnTime;
            attackManager.GetPetAttack().Upgrade(0.1f, currentSpawn * 0f);
        }

        if (attackManager.GetFlameAttack().isActive)
        {
            float currentSpawn = attackManager.GetFlameAttack().spawnTime;
            attackManager.GetFlameAttack().Upgrade(0.05f, currentSpawn * 0f);
        }
    }
}