using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("매니저 접근")]
    SoundManager sManager;
    public GameObject feverManager;
    public GameObject scoreManager;

    [Header("UI 오브젝트(버튼)")]
    public Button button;
    public Button restratButton;

    [Header("UI 오브젝트(부모)")]
    public GameObject introObj;
    public GameObject startGameUI;
    public GameObject startGame;
    public GameObject gameOverUI;

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



    public BossSpawner uIsBoss;
    public bool isBossAnim;

    [Header("보스 / 몬스터 스포너 / 플레이어 관련 오브젝트")]
    public GameObject bossSpawner;
    public GameObject PlayerPos;
    public GameObject[] MonsterSpawner;


    private void Awake()
    {
        sManager = FindFirstObjectByType<SoundManager>();
    }

    private void Start()
    {
        button.onClick.AddListener(StartGame);
        restratButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        introObj.SetActive(false);

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

        PlayerPos.transform.position = new Vector3(0, -5.4f, 0);
        MonsterSpawner[0].SetActive(true);
        MonsterSpawner[1].SetActive(true);
        MonsterSpawner[2].SetActive(true);

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
    }

    private void Update()
    {
        HpUISetting();

        if (playerController.hp <= 0)
        {
            GameOver();
        }

        if (uIsBoss.isBossSpawning == true && !isBossAnim)
        {
            isBossAnim = true;
            bossProduction.SetActive(true);
            bossFadeIn.SetActive(true);
        }

        if (isBossAnim)
        {
            Animator bossAnim = bossFadeIn.GetComponent<Animator>();
            AnimatorStateInfo currentStateInfo = bossAnim.GetCurrentAnimatorStateInfo(0);

            // 페이드인 애니메이션이 완료되었는지 확인
            if (currentStateInfo.normalizedTime >= 1.0f)
            {
                // warringUI의 자식 오브젝트들을 활성화
                foreach (Transform child in bossProduction.transform)
                {
                    child.gameObject.SetActive(true);
                }
                // Invoke는 한 번만 호출되도록, 애니메이션 완료 시점에 호출
                Invoke("BossAnimEnd", 3f);
            }
        }
    }

    public void GameOver()
    {
        introObj.SetActive(false);

        startGameUI.SetActive(false);
        startGame.SetActive(false);
        scoreManager.SetActive(false);
        feverManager.SetActive(false);

        gameOverUI.SetActive(true);

        sManager.EventSoundPlay("GameOver");
        ClearAllMonsters();
        ClearAllItems();
        ClearAllEnemyBullets();
    }

    private void ClearAllMonsters()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Monster monster in monsters)
        {
            Destroy(monster.gameObject);
        }
    }

    private void ClearAllItems()
    {
        BonusItem[] item = FindObjectsByType<BonusItem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (BonusItem items in item)
        {
            Destroy(items.gameObject);
        }
    }

    private void ClearAllEnemyBullets()
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
    }


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
        // 페이드 아웃 애니메이션이 끝난 후 오브젝트를 비활성화하기 위해 Invoke 사용
        // '1f'는 페이드 아웃 애니메이션의 예상 지속 시간입니다. 실제 애니메이션 길이에 맞춰 조정해주세요.
        warringEffect.SetActive(false);
        bossText.SetActive(false);
        bossImage.SetActive(false);
        Invoke("DeactivateBossUI", 0.5f);

    }

    private void DeactivateBossUI()
    {
        isBossAnim = false;
        bossFadeIn.SetActive(false);

    }

}