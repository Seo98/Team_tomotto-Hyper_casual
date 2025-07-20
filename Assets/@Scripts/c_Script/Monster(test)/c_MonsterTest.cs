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
        c_nowhp = c_monhp; // 현재 hp는 몬스터의 hp
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
            //추후에 데미지만큼 받도록 takeDamage 함수 실행으로 변경
            c_nowhp -= 1; //현재 체력 = 현재 체력- 1;
            c_monhp = c_nowhp;
        }

  
    }
}
