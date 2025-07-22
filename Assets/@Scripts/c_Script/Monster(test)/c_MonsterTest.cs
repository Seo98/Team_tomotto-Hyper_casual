using UnityEngine;

public class c_MonsterTest : MonoBehaviour
{

    public int c_monhp = 3;
    public int c_nowhp;
    // Dev_S : �Ʒ� �ֵ��� �� c_�� ���������ʳ���...????????????????????????????????????????????? �׽�Ʈ�� ���Ͷ� �׷�����?
    public Vector3 nowPos;
    public bool isDead = false;

    SpriteRenderer sr;
    c_MonsterDropItem dropIt;

    void Start()
    {
        dropIt = GameObject.Find("DropManager").GetComponent<c_MonsterDropItem>();
        c_nowhp = c_monhp; // ���� hp�� ������ hp
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(c_monhp == 0 && !isDead ) // DEV_S : �̰� ������Ʈ���� ���� �ʿ��ұ��?
        {
            nowPos = this.transform.position;
            dropIt.DropItem(nowPos);                        
            isDead = true;
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //if (feverTime.c_isFever)
        //{
        //    this.gameObject.SetActive(false);
        //    return;
        //}

        if (other.CompareTag("fireball"))
        {
            //���Ŀ� ��������ŭ �޵��� takeDamage �Լ� �������� ����
            c_nowhp -= 1; //���� ü�� = ���� ü��- 1;
            c_monhp = c_nowhp;
        }
    }
}
