using UnityEngine;

public class c_MonsterDropItem : MonoBehaviour
{
    [SerializeField] GameObject[] c_items;

    // B_FIX : Dev_Seo
    //GameObject target = GameObject.Find(playerName);    // �ν����ͻ� �÷��̾� �̸� �˻�
    // >>>>
    //���� ü�� �޾ƿ�
    //c_MonsterTest c_monster; //���� ��ũ��Ʈ
    // A_Temp_Fix : Dev_Seo
    

    private void Start()
    {
        // B_Fix : Dev_Seo
        //c_monster = FindFirstObjectByType<c_MonsterTest>();
        //���� ���� ü���� 0�� �� ���� ��ġ���� ��� ������
        // A_Temp_Fix : Dev_Seo
        
    }

    private void Update()
    {
        //if(c_monsterhp.c_monhp == 0)
        //{
        //    DropItem(c_monsterhp.nowPos);
        //}
    }

    public void DropItem(Vector3 dropPos)
    {
        int Index = Random.Range(0,2);
        
        GameObject c_item = Instantiate(c_items[Index], dropPos, Quaternion.identity);


        // B_Fix : Dev_Seo
        //h_enemy.h_isDead = false;
        // >>>>
        // A_Temp_Fix : Dev_Seo
        // >> Enemy ��ũ��Ʈ���� ó���ϵ��� ����


        //Rigidbody2D c_itemRb = c_item.GetComponent<Rigidbody2D>();

        //c_itemRb.AddForceY(3f, ForceMode2D.Impulse);
    }

}
