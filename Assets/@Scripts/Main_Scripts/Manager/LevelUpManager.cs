using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    private enum skillType { AtkUp, AtkCountUp, Harpoon, Flame, Ice, Pet, Heal }
    SoundManager sManager;

    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] public GameObject levelUpUI;
    [SerializeField] private Transform skillContants;

    [Header("스킬 데이터")]
    [SerializeField] private List<skillType> allSkills;
    [SerializeField] private GameObject[] skillPrefabs;

    public static LevelUpManager Instance { get; set; }

    [Header("레벨업 대상 연결")]
    [SerializeField] private PlayerController player;

    public int curExp = 0;
    public int curLevel = 1;
    public float maxExp = 100;

    private Dictionary<skillType, int> skillLevels = new Dictionary<skillType, int>();
    private const int AtkCountUpMax = 2;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        levelText.text = "Lv : 1";
        levelUpUI.SetActive(false);
        sManager = FindFirstObjectByType<SoundManager>();

        // 모든 스킬 레벨 0으로 초기화
        foreach (skillType skill in System.Enum.GetValues(typeof(skillType)))
        {
            skillLevels[skill] = 0;
        }
    }

    public void AddExp(int expAmount)
    {
        curExp += expAmount;

        if (curExp >= maxExp)
        {
            LevelUp();
            SkillUp();
        }
    }

    private void LevelUp()
    {
        curLevel++;
        curExp = 0;
        maxExp += maxExp/3f; // 레벨에 따른 필요 경험치량 증가
        LevelDisplay();
    }

    public void LevelDisplay()
    {
        levelText.text = "Lv : " + curLevel;
    }

    public void LevelInit()
    {
        curLevel = 1;
        curExp = 0;
        maxExp = 100;
        levelText.text = "Lv : " + curLevel;
    }

    private void SkillUp()
    {
        levelUpUI.SetActive(true);
        sManager.EventSoundPlay("Level up"); //레벨업 사운드. 위치 옮기셔도 됩니다 (Dev_C)

        foreach (Transform child in skillContants)
        {
            Destroy(child.gameObject);
        }

        List<skillType> selectedSkills = GetRandomSkills(3);

        foreach (skillType skill in selectedSkills)
        {
            int index = (int)skill;
            GameObject prefab = skillPrefabs[index];

            GameObject buttonObj = Instantiate(prefab, skillContants);

            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                ApplySkill(skill);
                levelUpUI.SetActive(false);
                Time.timeScale = 1f;
            });
        }

        Time.timeScale = 0f;
    }

    private List<skillType> GetRandomSkills(int count)
    {
        List<skillType> tempList = new List<skillType>(allSkills);

        // AtkCountUp 제한 체크
        if (skillLevels[skillType.AtkCountUp] >= AtkCountUpMax)
        {
            tempList.Remove(skillType.AtkCountUp);
        }

        List<skillType> result = new List<skillType>();
        int actualCount = Mathf.Min(count, tempList.Count);

        for (int i = 0; i < actualCount; i++)
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

        // 카운트 증가
        skillLevels[skill]++;

        switch (skill)
        {
            case skillType.AtkUp:
                atkManager.GetBasicAttack().Upgrade(0.5f, 0.15f);
                break;
            case skillType.AtkCountUp:
                atkManager.GetBasicAttack().UpgradeProjectileCount();
                break;
            case skillType.Harpoon:
                atkManager.HarpoonAttack().Upgrade(1.2f, 0f);
                break;
            case skillType.Flame:
                atkManager.GetFlameAttack().Upgrade(0.1f, 0.1f);
                break;
            case skillType.Ice:
                atkManager.GetIceAttack().Upgrade(0.4f, 0.15f);
                break;
            case skillType.Pet:
                atkManager.GetPetAttack().Upgrade(0.2f, 0f);
                break;
            case skillType.Heal:
                player.hp = Mathf.Min(player.hp + 1, player.maxHp);
                break;
        }
    }
}