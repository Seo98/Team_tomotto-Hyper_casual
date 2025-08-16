using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    // Dev_H : 경험치와 레벨 관리, 레벨에 따른 능력치 강화 (현재 공격력과 공격속도)를 다루는 스크립트
    private enum skillType { AtkUp, AtkCountUp, Harpoon, Flame, Ice, Pet }
    SoundManager sManager;

    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private Transform skillContants;

    [Header("스킬 데이터")]
    [SerializeField] private List<skillType> allSkills;
    [SerializeField] private GameObject[] skillPrefabs;

    public static LevelUpManager Instance { get; set; }

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

        levelText.text = "Lv : 1"; // Dev_H: 초기 레벨 표시
        levelUpUI.SetActive(false);
        sManager = FindFirstObjectByType<SoundManager>();
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
            SkillUp();
        }
    }

    // Dev_H: 레벨업하고 경험치와 최대경험치 초기화
    private void LevelUp()
    {
        curLevel++;
        curExp = 0;
        maxExp += maxExp / 2f; ; // 점점 20%씩 많은 경험치 필요하도록 증가

        LevelDisplay();

        Debug.Log($"Level Up! 현재 레벨: {curLevel}");
    }

    public void LevelDisplay()
    {
        levelText.text = "Lv : " + curLevel; // Dev_H: 현재 레벨 표시
    }

    public void LevelInit()
    {
        curLevel = 1;
        curExp = 0;
        levelText.text = "Lv : " + curLevel; // 초기화된 레벨로 표시
    }

    // Dev_H: 레벨업시 능력치 상승
    private void SkillUp()
    {
        levelUpUI.SetActive(true);
        sManager.EventSoundPlay("Level up"); //레벨업 사운드. 위치 옮기셔도 됩니다 (Dev_C)

        // 기존 버튼 삭제
        foreach (Transform child in skillContants)
        {
            Destroy(child.gameObject);
        }

        // 랜덤 스킬 3개 선택
        List<skillType> selectedSkills = GetRandomSkills(3);

        // 버튼 생성
        foreach (skillType skill in selectedSkills)
        {
            int index = (int)skill; // enum 순서를 index로 사용
            GameObject prefab = skillPrefabs[index];

            GameObject buttonObj = Instantiate(prefab, skillContants);

            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplySkill(skill);
                levelUpUI.SetActive(false);
                Time.timeScale = 1f;
            });
        }

        // 게임 일시정지
        Time.timeScale = 0f;
    }

    private List<skillType> GetRandomSkills(int count)
    {
        List<skillType> tempList = new List<skillType>(allSkills);
        List<skillType> result = new List<skillType>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            result.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);
        }

        return result;
    }

    private void ApplySkill(skillType skill)
    {
        AttackManager atkManager = AttackManager.Instance;
        Debug.Log("스킬획득 실행");

        switch (skill)
        {
            case skillType.AtkUp:
                atkManager.GetBasicAttack().Upgrade(1f, 0.3f);
                Debug.Log("일반공격 강화");
                break;
            case skillType.AtkCountUp:
                atkManager.GetBasicAttack().UpgradeProjectileCount();
                Debug.Log("일반공격횟수 강화");
                break;
            case skillType.Harpoon:
                atkManager.HarpoonAttack().Upgrade(1.5f, 0.2f);
                Debug.Log("작살공격 획득");
                break;
            case skillType.Flame:
                atkManager.GetFlameAttack().Upgrade(0.05f, 0.15f);
                Debug.Log("화염공격 획득");
                break;
            case skillType.Ice:
                atkManager.GetIceAttack().Upgrade(0.25f, 0.2f);
                Debug.Log("얼음공격 획득");
                break;
            case skillType.Pet:
                atkManager.GetPetAttack().Upgrade(0.5f, 0f);
                Debug.Log("펫 획득");
                break;
        }
    }
}
