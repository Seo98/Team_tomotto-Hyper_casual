using UnityEngine;



/// <summary>
/// 모든 몬스터의 기본이 되는 부모 클래스
/// </summary>
public abstract class Monster : MonoBehaviour
{
    [Header("기본 능력치")]
    public float speed;
    public float hp;

    // 컴포넌트 및 참조
    private c_MonsterDropItem dropIt;
    public s_PlayerController s_player;

    protected Vector3 dir; // 이동 방향
    int dropPer;

    public float levelUpTime = 30;
    public float currentTime;

    protected virtual void OnEnable()
    {
        // 공통 초기화
        dropIt = GameObject.Find("DropManager").GetComponent<c_MonsterDropItem>();
        s_player = GameObject.FindWithTag("Player").GetComponent<s_PlayerController>();
        
        // 자식 클래스에서 구현할 개별 몬스터 초기화 호출
        Initialize();
    }

    protected abstract void Initialize();

    protected virtual void MonsterLevelUp()
    {
        currentTime += 1 * Time.deltaTime;
        if(currentTime > levelUpTime)
        {
            hp += 1f;
            currentTime = 0;
        }

    }


    protected virtual void Dead()
    {
        Vector3 nowPos = this.transform.position;
        if (dropIt != null)
        {
            dropPer = Random.Range(1, 10);

            if(dropPer > 8)dropIt.DropItem(nowPos);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 플레이어의 공격에 맞았을 때
        if (other.gameObject.CompareTag("fireball"))
        {
            hp -= s_player.c_Damage;
            if (hp <= 0)
            {
                Dead();
                return;
            }
        }

        // 플레이어와 직접 충돌했을 때
        if (other.gameObject.CompareTag("Player"))
        {
            Dead();

            if (s_player.c_fever.c_isFever) return;

            else if (s_player.c_isShield)
            {
                s_player.BreakShield();
                return;
            }
            s_player.c_hp -= 1f;
        }
    }
}