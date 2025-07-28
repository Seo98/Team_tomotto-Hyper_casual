using UnityEngine;

public class EXPManager : MonoBehaviour
{
    public static EXPManager Instance { get; set; }

    // Dev_H: PlayerController에 있는 공격력, 공격속도 올리기 위해
    [Header("레벨업 대상 연결")]
    [SerializeField] private PlayerController player;

    // Dev_H: 현재 경혐치와 레벨, 최대경험치
    public int curExp = 0;
    public int curLevel = 1;
    public float maxExp = 100;  

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Dev_H: Monster 스크립트에서 GiveExp 함수에서 기능
    public void AddExp(int expAmount)
    {
        // Dev_H: expAmount가 주는 경험치인데 몬스터마다 적용돼있음
        curExp += expAmount;
        Debug.Log($"Exp +{expAmount}, 현재: {curExp}");

        if (curExp >= maxExp)
        {
            LevelUp();
            StateUp();
        }
    }

    // Dev_H: 레벨업하고 경험치와 최대경험치 초기화
    private void LevelUp()
    {
        curLevel++;
        curExp = 0;
        maxExp += maxExp / 2f; ; // 점점 20%씩 많은 경험치 필요하도록 증가

        Debug.Log($"Level Up! 현재 레벨: {curLevel}");
    }

    // Dev_H: 레벨업시 능력치 상승
    // 현재는 가하는 데미지 +1, 공격속도 현재속도에 20%씩 증가
    // 레벨업 시 어떤 능력치를 올릴지 선택지가 나오는 것 도 좋겠죠
    private void StateUp()
    {
        player.damage += 1f;
        player.spawnTime -= player.spawnTime / 5;
    }
}
