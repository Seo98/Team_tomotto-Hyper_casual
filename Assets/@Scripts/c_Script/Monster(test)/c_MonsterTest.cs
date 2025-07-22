using UnityEngine;

public class c_MonsterTest : MonoBehaviour
{

    public int c_monhp = 3;
    public int c_nowhp;
    // Dev_S : 아래 애들은 왜 c_로 시작하지않나요...????????????????????????????????????????????? 테스트용 몬스터라서 그런가요?
    public Vector3 nowPos;
    public bool isDead = false;

    SpriteRenderer sr;
    c_MonsterDropItem dropIt;

    void Start()
    {
        dropIt = GameObject.Find("DropManager").GetComponent<c_MonsterDropItem>();
        c_nowhp = c_monhp; // 현재 hp는 몬스터의 hp
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {        
        if(c_monhp == 0 && !isDead ) // DEV_S : 이거 업데이트에서 굳이 필요할까요?
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
            //추후에 데미지만큼 받도록 takeDamage 함수 실행으로 변경
            c_nowhp -= 1; //현재 체력 = 현재 체력- 1;
            c_monhp = c_nowhp;
        }
    }
}
