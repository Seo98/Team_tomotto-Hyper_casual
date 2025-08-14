using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance;

    [Header("참조")]
    public FeverTimeManager feverManager;
    public BossSpawner bossSpawner;

    [Header("프리팹들")]
    public GameObject basicProjectilePrefab;
    public GameObject flameProjectilePrefab;
    public GameObject iceProjectilePrefab;
    public GameObject petProjectilePrefab;
    public GameObject harpoonProjectilePrefab;

    [Header("발사 위치들")]
    public Transform[] firePositions;

    // Attack 컴포넌트들 참조
    private BasicAttack basicAttack;
    private IceAttack iceAttack;
    private HarpoonAttack harpoonAttack;
    private PetAttack petAttack;
    private FlameAttack flameAttack;

    public BasicAttack GetBasicAttack() { return basicAttack; }
    public IceAttack GetIceAttack() { return iceAttack; }
    // public IceAttack HarpoonAttack() { return harpoonAttack; }
    // public IceAttack PetAttack() { return petAttack; }
    // public IceAttack FlameAttack() { return flameAttack; }

    private bool feverSpeedApplied = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 컴포넌트들 가져오기
        basicAttack = GetComponent<BasicAttack>();
        flameAttack = GetComponent<FlameAttack>();
        iceAttack = GetComponent<IceAttack>();
        petAttack = GetComponent<PetAttack>();
        harpoonAttack = GetComponent<HarpoonAttack>();
    }
    private void Start()
    {
        basicAttack.Activate();
    }

    private void Update()
    {
        if (feverManager.isFever && bossSpawner.isBoss && !feverSpeedApplied)
        {
            ApplyFeverSpeedBoost(); // 한 번만 실행
            feverSpeedApplied = true; // 플래그 설정
        }

        if ((!feverManager.isFever || !bossSpawner.isBoss) && feverSpeedApplied)
        {
            RemoveFeverSpeedBoost(); // 한 번만 복구
            feverSpeedApplied = false; // 플래그 해제
        }
    }




    // 각 타입에 맞게끔 추가 할당해주면 됩니당
    // 빨라졌다가 다시 원래 스폰타임
    public void ApplyFeverSpeedBoost()
    {
        if (basicAttack.isActive) basicAttack.spawnTime *= 0.5f;
        if (iceAttack.isActive) iceAttack.spawnTime *= 0.5f;
        if (basicAttack.isActive) basicAttack.spawnTime *= 0.5f;
        if (iceAttack.isActive) iceAttack.spawnTime *= 0.5f;
        if (basicAttack.isActive) basicAttack.spawnTime *= 0.5f;
        // ...
    }


    public void RemoveFeverSpeedBoost()
    {
        if (basicAttack.isActive) basicAttack.spawnTime *= 2f;
        if (iceAttack.isActive) iceAttack.spawnTime *= 2f;
        if (basicAttack.isActive) basicAttack.spawnTime *= 2f;
        if (iceAttack.isActive) iceAttack.spawnTime *= 2f;
        if (basicAttack.isActive) basicAttack.spawnTime *= 2f;
        // ...
    }


    // 초기화
    // 추가타입 생기면 여기서도 그렇게 처리하면댑니당.
    public void InitializeAttacks()
    {
        // 모든 공격들 초기화
        basicAttack.ResetToDefault();
        if (iceAttack != null) iceAttack.ResetToDefault();
        if (flameAttack != null) flameAttack.ResetToDefault();
        if (petAttack != null) petAttack.ResetToDefault();
        if (harpoonAttack != null) harpoonAttack.ResetToDefault();

        // BASIC만 활성화
        // FIXME : -> 인트로 씬에서 계속 발사되는 현상이 있어서 UI 매니저에서 스타트함수 호출시 활성하도록 설정해야하는데
        // 스타트할떄 한번 싹 클리어해줘서 크게 이슈없을경우 현상유지, 이슈있을시 변경
        basicAttack.Activate(); 
    }


    public PetAttack GetPetAttack() { return petAttack; }
    public FlameAttack GetFlameAttack() { return flameAttack; }

}