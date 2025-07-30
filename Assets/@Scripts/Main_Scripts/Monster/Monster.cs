using UnityEngine;



/// <summary>
/// 모든 몬스터의 기본이 되는 부모 클래스
/// </summary>
public abstract class Monster : MonoBehaviour
{
    // Dev_S: 각 개체의 기능은 자식 스크립트에서 UPDATE 에 할당되있는것만 기능됩니다.
    [Header("기본 능력치")]
    public float speed;
    public float hp;
    SoundManager sManager;

    // 컴포넌트 및 참조
    private MonsterDropItem dropIt; // 은주님쪽 라인 참조
    public PlayerController player; // 은주님쪽 라인 참조

    protected Vector3 dir; // 이동 방향
    int dropPer;

    public float monsterLevelUpTime = 30;
    public float currentTime;

    // Dev_H: 경험치 부여량, 각 몬스터 스크립트 참조
    public int expAmount;

    protected virtual void OnEnable()
    {
        // 공통 초기화
        dropIt = GameObject.Find("DropManager").GetComponent<MonsterDropItem>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        sManager = FindFirstObjectByType<SoundManager>();

        // Dev_S: 자식 클래스에서 구현할 개별 몬스터 초기화 호출
        Initialize();
    }
    
    protected abstract void Initialize(); // Dev_S: 여기서 코드쓰면 안되고 자식에서 오버라이딩 쓰고 쓰셔야해요 애들마다 맨처음 hp 달라야할거같아서 

    protected virtual void MonsterLevelUp() // Dev_S: 몬스터 레벨업 기능 // 웨이브 레벨업 시스템
    {
        currentTime += 1 * Time.deltaTime;
        if(currentTime > monsterLevelUpTime)
        {
            hp += 1f;
            currentTime = 0;
        }
    }

    protected virtual void Dead() // Dev_S: 사망기능 (드랍 포함)
    {
        Vector3 nowPos = this.transform.position;
        if (dropIt != null) // 
        {
            dropPer = Random.Range(1, 10); // 확률

            if (dropPer > 8)dropIt.DropItem(nowPos);
        }

        Destroy(gameObject);
        GiveExp();  // Dev_H: 경험치 부여하는 함수 호출
    }

    // Dev_S:공격 계산식 충돌 관련 계산로직
    private void OnCollisionEnter2D(Collision2D other)
    {
        // 플레이어의 공격에 맞았을 때
        if (other.gameObject.CompareTag("fireball"))
        {
            hp -= player.damage;
            sManager.EventSoundPlay("fire_arrow_hit");
            if (hp <= 0)
            {
                Dead();
                return;
            }
        }

        // 플레이어와 직접 충돌했을 때
        if (other.gameObject.CompareTag("Player"))
        {
            sManager.EventSoundPlay("hitting");
            Dead();

            if (player.fever.isFever) return;

            else if (player.isShield)
            {
                player.BreakShield();
                return;
            }
            player.hp -= 1f;
        }
    }

    protected void GiveExp() // Dev_H: 경험치 부여하는 기능
    {
        EXPManager.Instance.AddExp(expAmount);
    }
}