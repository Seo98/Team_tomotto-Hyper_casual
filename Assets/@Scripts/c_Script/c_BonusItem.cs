using UnityEngine;

public class c_BonusItem : MonoBehaviour
{
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType c_bonusItem;

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

    }

    void c_AttackUP()//�ܼ��� ������ �����ؼ� ���ǵ� �ø���
    {

    }
}
