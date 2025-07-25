using UnityEngine;

public class c_BonusItem : MonoBehaviour
{
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType c_bonusItem;

    ItemManager c_itManager;
    s_PlayerController player;

    SpriteRenderer c_sr;
    public c_Cannonball c_fireball;

    bool isShield;


    public Vector3 dir;
    public float speed = 3;
    GameObject target;

    private void Awake()
    {
        //B_Fix : Dev_Seo
        //player = GameObject.Find("Player").GetComponent<s_PlayerController>();
        // >>>>
        // A_Temp_Fix : Dev_Seo
        player = GameObject.FindWithTag("Player").GetComponent<s_PlayerController>();

        c_sr = this.GetComponent<SpriteRenderer>();
        c_itManager = FindFirstObjectByType<ItemManager>(); 
    }

    private void OnEnable()
    {
        
    }


    public void Update()
    {
        GameObject target = GameObject.FindWithTag("Player"); //
        transform.position += dir * speed * Time.deltaTime; // �ڼ�
        if (target != null)
        {
            dir = (target.transform.position - transform.position).normalized;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //�÷��̾ �� �������� �Դ´ٸ�
    {
        if(other.CompareTag("Player"))
        {
            if (this.c_bonusItem == BonusType.SHEILD)
            {
                c_Shield();
                Destroy(gameObject);
            }
            if(this.c_bonusItem == BonusType.ATTACKUP)
            {
                c_AttackUP();
                Destroy(gameObject);
            }
        }
    }

    void c_Shield() //�Ķ� ��Ʈ �߰��� ����� �÷��̾� �ֺ��� �Ķ��� �� �����... ��ֹ� �ε����� �Ķ��� ��Ʈ�� �� ���� ����
    {
        Debug.Log("isShield");
        player.c_isShield = true;
        c_itManager.c_ShiledHeart.SetActive(true);
        c_itManager.c_Shiledimage.SetActive(true);                     
        //���� ������ ����
    }

    void c_AttackUP()//�ܼ��� ������ ���� �ֱ� �������� 
    {
        c_fireball.c_speed = 13f; //���� ���ǵ� ��
        player.c_spawnTime = 1f; //���� ���� �ð� ����

        // B_Fix : Dev Seo
        //c_fireball.c_Damage = 2; //������ ��
        player.c_Damage += 1f;
        // >>>>
        // A_Temp_Fix : Dev_Seo
    }
}
