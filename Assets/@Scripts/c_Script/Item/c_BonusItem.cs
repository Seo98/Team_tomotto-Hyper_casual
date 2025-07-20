using UnityEngine;

public class c_BonusItem : MonoBehaviour
{
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType c_bonusItem;

    public GameObject c_ShiledHeart;
    public GameObject c_Shiledimage;

    SpriteRenderer c_sr;
    s_PlayerController player;
    c_Cannonball c_fireball;

    bool isShield;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<s_PlayerController>();
        c_sr = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            c_sr.enabled = false;
            if (this.c_bonusItem == BonusType.SHEILD)
            {
                c_Shield();               
            }
            if(this.c_bonusItem == BonusType.ATTACKUP)
            {
                c_AttackUP();
            }
        }
    }

    void c_Shield() //�Ķ� ��Ʈ �߰��� ����� �÷��̾� �ֺ��� �Ķ��� �� �����... ��ֹ� �ε����� �Ķ��� ��Ʈ�� �� ���� ����
    {
        
        while(isShield)
        {
            c_ShiledHeart.SetActive(true);
            c_Shiledimage.SetActive(true);
            break;
        }        
        //���� ������ ����
    }

    void c_AttackUP()//�ܼ��� ������ ���� �ֱ� �������� 
    {
        c_fireball.c_speed = 13f; //���� ���ǵ� ��
        player.c_spawnTime = 1f; //���� ���� �ð� ����
        c_fireball.c_Damage = 2; //������ ��
    }
}
