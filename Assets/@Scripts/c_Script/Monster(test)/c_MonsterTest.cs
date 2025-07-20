using UnityEngine;

public class c_MonsterTest : MonoBehaviour
{

    public int c_monhp = 3;
    public int c_nowhp;
    public Vector3 nowPos;
    c_MonsterDropItem dropIt;
    public bool isDead = false;

    SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dropIt = GameObject.Find("ItemManager").GetComponent<c_MonsterDropItem>();
        c_nowhp = c_monhp; // ���� hp�� ������ hp
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(c_monhp == 0 && !isDead )
        {
            nowPos = this.transform.position;
            sr.enabled = false;
            dropIt.DropItem(nowPos);                        
            isDead = true;
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
