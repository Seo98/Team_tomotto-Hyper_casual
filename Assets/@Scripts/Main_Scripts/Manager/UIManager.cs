using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("매니저 접근")]
    SoundManager sManager;
    public GameObject feverManager;
    public GameObject scoreManager;

    [Header("UI 오브젝트(버튼)")]
    public Button startbutton;
    public Button restratButton;
    public Button homeButton;
    public Button nextButton;

    [Header("UI 오브젝트(부모)")]
    public GameObject introObj;
    public GameObject startGameUI;
    public GameObject startGame;
    public GameObject gameOverUI;
    public GameObject gameClearUI;

    [Header("UI 오브젝트(체력)")]
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    [Header("UI 잉크효과 오브젝트")]
    public InkEffect ink;
    public PlayerController playerController;


    [Header("보스 UI")]
    public GameObject bossProduction; // 부모
    public GameObject bossFadeIn; // 페이드효과
    public GameObject warringEffect;
    public GameObject bossText;
    public GameObject bossImage;


    [Header("보스 연출용 조건값")]
    public BossSpawner uIsBoss;
    public bool isBossAnim;
    public bool oneShot;

    [Header("보스 / 몬스터 스포너 / 플레이어 관련 오브젝트")]
    public GameObject bossSpawner;
    public GameObject PlayerPos;
    public GameObject[] MonsterSpawner;

    [Header("스테이지 관리")]
    public int currentStage = 1;
    public float stageHPIncrease = 1f;


    #region 사운드 관련 조건값
    private bool isWarringSound;
    #endregion

    private void Awake()
    {
        sManager = FindFirstObjectByType<SoundManager>();
    }

    private void Start()
    {
        startbutton.onClick.AddListener(StartGame);
        restratButton.onClick.AddListener(StartGame);
        Pause_UI home = FindFirstObjectByType<Pause_UI>();
        homeButton.onClick.AddListener(home.GoHome);
        nextButton.onClick.AddListener(NextStage);
    }

    private void StartGame()
    {
        PlayerPos.transform.position = new Vector3(0, -5.4f, 0); // 유저 초기위치 초기화

        sManager.BgmSoundPlay("Gb 1"); // 스타트시 브금재생

        introObj.SetActive(false); // 필요없는 UI제거

        // 필요한 UIOn
        startGameUI.SetActive(true);
        startGame.SetActive(true);
        scoreManager.SetActive(true);
        feverManager.SetActive(true);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);


        gameOverUI.SetActive(false);
        ink.SetAlpha(0f); // 잉크 남아있는 이슈 사전처리

        // UI랑 상관 없는것
        bossSpawner.SetActive(true);
        uIsBoss = bossSpawner.GetComponent<BossSpawner>();

        MonsterSpawner[0].SetActive(true);
        MonsterSpawner[1].SetActive(true);
        MonsterSpawner[2].SetActive(true);
        //
        MonsterSpawner[3].SetActive(true);
        //
        MonsterSpawner[4].SetActive(true);

        // 피버 조기화 이슈
        FeverTimeManager fv = feverManager.GetComponent<FeverTimeManager>();
        fv.isFever = false;
        fv.player.moveSpeed = 0.2f;
        fv.playColl.isTrigger = false;
        fv.feverImage.fillAmount = 0f;
        fv.feverStartImage.SetActive(false);

        // 보스 UI 알파값 초기화
        Image fadeImage = bossFadeIn.GetComponent<Image>();
        if (fadeImage != null)
        {
            Color imageColor = fadeImage.color;
            imageColor.a = 1f;
            fadeImage.color = imageColor;
        }
        oneShot = false;

        // 사운드 초기화
        sManager.isGameEnd = false;
        isWarringSound = false;

        // 스테이지 // 몬스터 피통 초기화
        currentStage = 1;
        Monster.stageHPBonus = 0f;

        //플레이어 초기화
        playerController.hp = 3f;
        AttackManager.Instance.InitializeAttacks();
        LevelUpManager.Instance.LevelInit();
        
    }

    public void GameOver()
    {
        introObj.SetActive(false);

        startGameUI.SetActive(false);
        startGame.SetActive(false);
        scoreManager.SetActive(false);
        feverManager.SetActive(false);

        gameOverUI.SetActive(true);
        bossProduction.SetActive(false);

        if (sManager.isGameEnd == false)
        {
            sManager.isGameEnd = true;
            sManager.EventSoundPlay("GameOver");
        }

        ClearAllMonsters();
        ClearAllItems();
        ClearAllEnemyBullets();
        ClearAllBullets();

        currentStage = 1;
    }

    public void GameClear()
    {
        gameClearUI.SetActive(true);
        uIsBoss.gameObject.SetActive(false);
        bossProduction.gameObject.SetActive(false);


        isWarringSound = false;

        ClearAllEnemyBullets(); // dev_s : 지금은 일괄로 부셔버려서 바로 사라지는데,
                           // 시간되면 보스가 죽으면 모든 총알도 애니메이션 효과 진행 후 사라지면 좋을듯합니다
    }

    public void NextStage()
    {
        sManager.BgmSoundPlay("Gb 1"); // 넥스트 진행시 브금재생
        currentStage++;
        Monster.stageHPBonus = (currentStage - 1) * stageHPIncrease;
        Debug.Log($"Stage {currentStage} Start! Monster HP Bonus: +{Monster.stageHPBonus}");

        uIsBoss.gameObject.SetActive(true);
        //
        MonsterSpawner[0].SetActive(true);
        MonsterSpawner[1].SetActive(true);
        MonsterSpawner[2].SetActive(true);
        //
        MonsterSpawner[3].SetActive(true);
        //
        MonsterSpawner[4].SetActive(true);
        //

        gameClearUI.SetActive(false);
        oneShot = false;

        Image fadeImage = bossFadeIn.GetComponent<Image>();
        if (fadeImage != null)
        {
            Color imageColor = fadeImage.color;
            imageColor.a = 1f;
            fadeImage.color = imageColor;
        }
        oneShot = false;

        
    }


    private void Update()
    {
        HpUISetting();

        if (playerController.hp <= 0)
        {
            GameOver();
        }

        if (uIsBoss.isBossSpawning == true && !isBossAnim && oneShot == false)
        {
            isBossAnim = true;
            bossProduction.SetActive(true);
            bossFadeIn.SetActive(true);
            oneShot = true;

            foreach (GameObject spawner in MonsterSpawner)
            {
                spawner.SetActive(false);
            }
        }

        if (isBossAnim == true)
        {
            
            Animator bossAnim = bossFadeIn.GetComponent<Animator>();
            AnimatorStateInfo currentStateInfo = bossAnim.GetCurrentAnimatorStateInfo(0);


            if (isWarringSound == false)
            {
                sManager.BgmSoundStop();
                Debug.Log("사운드재생");
                sManager.EventSoundPlay("warning"); 
                isWarringSound = true;

                ClearMonsters();
                ClearEnemyBullets();
            }

            // 페이드인 애니메이션이 완료되었는지 확인
            if (currentStateInfo.normalizedTime >= 1.0f)
            {
                isBossAnim = false;
                Debug.Log("애니메이션 시작");
                foreach (Transform child in bossProduction.transform)
                {
                    child.gameObject.SetActive(true);
                }
                // warringUI의 자식 오브젝트들을 비활성화
                Invoke("BossAnimEnd", 3f);
            }
            
        }
    }

    #region 적총알 / 내총알 / 몬스터 / 아이템 / Destroy
    public void ClearAllMonsters()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Monster monster in monsters)
        {
             Destroy(monster.gameObject);
        }
    }

    // 보스제외 몬스터초기화
    public void ClearMonsters()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Monster monster in monsters)
        {
            if (!monster.CompareTag("Boss"))
            {
                Destroy(monster.gameObject);
            }
        }
    }

    public void ClearAllItems()
    {
        BonusItem[] item = FindObjectsByType<BonusItem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (BonusItem items in item)
        {
            Destroy(items.gameObject);
        }
    }


    public void ClearAllBullets()
    {
        BaseAttack[] BaseAttacks = FindObjectsByType<BaseAttack>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (BaseAttack baseAttack in BaseAttacks)
        {
            if (baseAttack.GetComponent<AttackManager>() == null)
            {
                Destroy(baseAttack.gameObject);
            }
        }

    }

    public void ClearAllEnemyBullets()
    {
        Inkball[] inkBalls = FindObjectsByType<Inkball>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Inkball inkBall in inkBalls)
        {
            Destroy(inkBall.gameObject);
        }

        EnemyBullet[] enemyBullets = FindObjectsByType<EnemyBullet>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (EnemyBullet enemyBullet in enemyBullets)
        {
            Destroy(enemyBullet.gameObject);
        }

        EnemyLazer[] enemyLasers = FindObjectsByType<EnemyLazer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (EnemyLazer enemylaser in enemyLasers)
        {
            Destroy(enemylaser.gameObject);
        }

        LazerWarring[] enemyLaserWarrings = FindObjectsByType<LazerWarring>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (LazerWarring enemyLaserWarring in enemyLaserWarrings)
        {
            Destroy(enemyLaserWarring.gameObject);
        }

        OrangeOctBullet[] orangeOctBullets = FindObjectsByType<OrangeOctBullet>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (OrangeOctBullet orangeOctBullet in orangeOctBullets)
        {
            Destroy(orangeOctBullet.gameObject);
        }
    }

    // 레이저 제외
    public void ClearEnemyBullets()
    {
        Inkball[] inkBalls = FindObjectsByType<Inkball>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Inkball inkBall in inkBalls)
        {
            Destroy(inkBall.gameObject);
        }

        EnemyBullet[] enemyBullets = FindObjectsByType<EnemyBullet>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (EnemyBullet enemyBullet in enemyBullets)
        {
            Destroy(enemyBullet.gameObject);
        }

        OrangeOctBullet[] orangeOctBullets = FindObjectsByType<OrangeOctBullet>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (OrangeOctBullet orangeOctBullet in orangeOctBullets)
        {
            Destroy(orangeOctBullet.gameObject);
        }
    }
    #endregion

    private void HpUISetting()
    {
        if (playerController.hp == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if (playerController.hp == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        if (playerController.hp == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
    }

    private void BossAnimEnd()
    {
        Animator bossAnim = bossFadeIn.GetComponent<Animator>();
        bossAnim.SetTrigger("isFadeOut");
        warringEffect.SetActive(false);
        bossText.SetActive(false);
        bossImage.SetActive(false);
        Invoke("DeactivateBossUI", 0.5f);

    }

    private void DeactivateBossUI()
    {
        bossFadeIn.SetActive(false);
        sManager.BgmSoundPlay("boss 1");
    }
    public void ShowDamage()
    {
        
    }

}