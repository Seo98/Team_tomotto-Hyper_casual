using UnityEngine;

public class c_MonsterDropItem : MonoBehaviour
{
    [SerializeField] GameObject[] c_items;

    //���� ü�� �޾ƿ�
    c_MonsterTest c_monster; //���� ��ũ��Ʈ

    

    private void Start()
    {
        c_monster = FindFirstObjectByType<c_MonsterTest>();
        //���� ���� ü���� 0�� �� ���� ��ġ���� ��� ������
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
        c_monster.isDead = false;

        //Rigidbody2D c_itemRb = c_item.GetComponent<Rigidbody2D>();

        //c_itemRb.AddForceY(3f, ForceMode2D.Impulse);
    }

}
