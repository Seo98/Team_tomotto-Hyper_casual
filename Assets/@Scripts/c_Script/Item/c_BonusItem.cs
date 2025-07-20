using UnityEngine;

public class c_BonusItem : MonoBehaviour
{
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType c_bonusItem;

    public GameObject c_ShiledHeart;
    public GameObject c_Shiledimage;

    s_PlayerController player;
    c_Cannonball c_fireball;

    bool isShield;

    private void Awake()
    {
        c_fireball = GameObject.Find("[bonus] canonball").GetComponent<c_Cannonball>();
        player = GameObject.Find("Player").GetComponent<s_PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(CompareTag("Player"))
        {
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
        isShield = true; 
        while(isShield)
        {
            c_ShiledHeart.SetActive(true);
            c_Shiledimage.SetActive(true);
            break;
        }
        isShield = false;


        //���� ������ ����
    }

    void c_AttackUP()//�ܼ��� ������ �����ؼ� ���ǵ� �ø���
    {
        c_fireball.speed = 7; //���� ���ǵ� ��
        player.c_spawnTime = 1f; //���� ���� �ð� ����

    }
}
