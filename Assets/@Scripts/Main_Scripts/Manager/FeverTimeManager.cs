using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeverTimeManager : MonoBehaviour
{

    // dev_s: 여기 은주님라인, 무브스피드 계수 건든거 말고는 손본거 없는걸로 기억합니다.
    public PlayerController player;

    public Collider2D playColl;
    public Image feverImage;
    public GameObject feverStartImage;
    public GameObject feverStartImage2;
    public bool isFever;
    public float nowGauge;


    // Dev_s: 추가수정 / 대쉬이펙트 관련
    public GameObject normalDash;
    public GameObject feverDash;

    private BossSpawner boss; // 진짜 큰일났다 스파게티가 되가고있어요

    private void OnEnable()
    {
        feverImage.fillAmount = 0;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playColl = player.GetComponent<Collider2D>();
        boss = GameObject.FindFirstObjectByType<BossSpawner>();

        // Dev_S // 초기화 관련
        feverDash.SetActive(false);
        normalDash.SetActive(true);

    }
    private void Update()
    {
        while (feverImage.fillAmount < 1)
        {
            feverImage.fillAmount += Time.deltaTime * 0.06f;
            nowGauge = feverImage.fillAmount;
            break;
        }
        if (!boss.isBossSpawning &&!isFever && feverImage.fillAmount >= 1)
            StartCoroutine(FeverTime());

        if (boss.isBossSpawning && !isFever && feverImage.fillAmount >= 1)
            StartCoroutine(FeverTime2());
    }

    IEnumerator FeverTime()
    {
        feverStartImage.SetActive(true);
        // Dev_S
        feverDash.SetActive(true);
        normalDash.SetActive(false);

        Debug.Log("IsFever");
        isFever = true;
        player.moveSpeed = 2f;
        
        yield return new WaitForSeconds(5f);

        isFever = false;
        player.moveSpeed = 0.2f;
        playColl.isTrigger = false;
        feverImage.fillAmount = 0f;
        feverStartImage.SetActive(false);
        // Dev_S
        feverDash.SetActive(false);
        normalDash.SetActive(true);
    }

    // Dev_S: 피버타임 둘중하나 선택 
    IEnumerator FeverTime2()
    {
        isFever = true;
        feverStartImage2.SetActive(true);
        yield return new WaitForSeconds(5f);

        feverStartImage2.SetActive(false);
        isFever = false;
        feverImage.fillAmount = 0f;
        // 로직구현은 프레이어컨트롤러에서 두개발사됨.. 큰일남.. 꼬이고꼬이고잇서..
    }
}