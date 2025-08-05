using UnityEditor;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    private UIManager uiManager;

    // 보스 스폰 관련 변수
    private float bossSpawnTime = 90f; // 스폰시간 조건 초기화
    public float currentTime; // 델타타임 플플 가중치 줄 변수
    public bool isBossSpawning = false; // 스폰했는지?
    private bool isBossMoveDone = false; // 이동완료했는지 ? 
    public GameObject bossPrefab;

    // 보스 이동 관련 변수
    private float startY = 8.5f; // 초기위치 -> 다음 게임 진행시에도 동일한 위치에서 동일한 연출을 위함.
    private float endY = 6.5f; // 
    private float moveSpeed = 2.0f;
    private Vector3 startPos;
    private Vector3 endPos;

    

    private void OnEnable() 
    {
        uiManager = FindFirstObjectByType<UIManager>();
        InitializeSpawner(); 
        // 스포너의 기초 값 초기화 
        // SetActive로 호출로 인해 OnEnable에 진행하여야함 
    }

    // 스포너 값 초기화
    private void InitializeSpawner()
    {
        currentTime = 0f;
        isBossSpawning = false;
        isBossMoveDone = false;

        
        boss = Instantiate(bossPrefab);
        boss.SetActive(false);
        
    }

    private void Update()
    {
        // 보스 이동이 완료되면 Update 로직을 더 이상 실행하지 않음
        if (isBossMoveDone) return;

        UpdateTimer(); // 시간 계속 가중치
        ProcessSpawning(); // 스폰 프로세스 진행
    }

    // 타이머 업데이트
    private void UpdateTimer()
    {
        currentTime += Time.deltaTime;
    }

    // 스폰 진행
    private void ProcessSpawning()
    {
        // 스폰 시간이 되었고 아직 스폰이 시작되지 않았다면 스폰 시작
        if (currentTime > bossSpawnTime && !isBossSpawning)
        {
            StartSpawning();
        }

        // 스폰이 시작되었고 아직 이동이 완료되지 않았다면 보스 이동
        if (isBossSpawning && !isBossMoveDone)
        {
            MoveBoss();
        }
    }

    // 스폰 시작
    private void StartSpawning()
    {
        isBossSpawning = true;
        uiManager.warringUI.SetActive(true);

        // 시작 및 종료 위치 설정
        startPos = new Vector3(transform.position.x, startY, transform.position.z);
        endPos = new Vector3(transform.position.x, endY, transform.position.z);

        // 보스 초기 위치 설정 및 활성화
        boss.transform.position = startPos;
        boss.SetActive(true);
    }

    // 보스 이동 처리
    private void MoveBoss()
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, endPos, moveSpeed * Time.deltaTime);

        // 목표 위치에 도달했는지 확인
        if (Vector3.Distance(boss.transform.position, endPos) < 0.01f)
        {
            FinalizeMovement();
        }
    }

    // 보스 이동 완료 처리
    private void FinalizeMovement()
    {
        boss.transform.position = endPos;
        isBossMoveDone = true;
        uiManager.warringUI.SetActive(false);
        // 이동이 완료되면 보스 조우시 나오는 UI도 꺼줌
        // 보스 AI 활성화
        // 보스 이동하면서 스킬쓰면 짜쳐서 사전처리해줌
        ActivateBossAI();
    }

    // 보스 AI 활성화
    private void ActivateBossAI()
    {
        /*
        Boss_R bossR = boss.GetComponent<Boss_R>();
        if (bossR == null)
        {
            Debug.LogError("Boss_R 스크립트를 찾을 수 없습니다.");
        }
        */
    }
}
