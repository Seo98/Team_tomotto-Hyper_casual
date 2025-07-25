using UnityEngine;
using UnityEngine.UI;

public class s_UIManager : MonoBehaviour
{
    [Header("매니저 접근")]
    public GameObject FeverManager;


    [Header("UI 오브젝트(버튼)")]
    public Button s_Button;
    public Button s_RestratButton;

    [Header("UI 오브젝트(부모)")]
    public GameObject s_IntroObj;
    public GameObject s_StartGameUI;
    public GameObject s_StartGame;
    public GameObject s_GameOverUI;
    public GameObject s_ScoreManag;
    public GameObject s_Heart1;
    public GameObject s_Heart2;
    public GameObject s_Heart3;


    public s_PlayerController s_PlayerController;


    private void Awake()
    {
        // FIXME : 현재 기능테스트, 추후 병합시 주석해제 예정
        /*
        // 게임 시작 인트로 활성화
        s_IntroObj.SetActive(true);

        // 게임 시작 UI 비활성화
        s_StartGameUI.SetActive(false);
        s_StartGame.SetActive(false);
        */
    }

    private void Start()
    {
        s_Button.onClick.AddListener(StartGame);
        s_RestratButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        s_IntroObj.SetActive(false); // 인트로UI

        s_StartGameUI.SetActive(true); // 게임시작 UI
        s_StartGame.SetActive(true); // 게임시작
        s_ScoreManag.SetActive(true); // 점수 관리
        FeverManager.SetActive(true); // 피버매니저
        s_Heart1.SetActive(true);
        s_Heart2.SetActive(true);
        s_Heart3.SetActive(true);


        s_GameOverUI.SetActive(false); // 게임오버 UI
    }

    private void Update()
    {

        // TODO : 병합시 조건식 변경
        /*
        if(s_GameManager.s_playerInfoRef.s_hp == 0)
        {

        }
        */
        HpUISetting();

        if (s_PlayerController.c_hp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        s_IntroObj.SetActive(false); // 인트로UI

        s_StartGameUI.SetActive(false); // 게임시작 UI
        s_StartGame.SetActive(false); // 게임시작
        s_ScoreManag.SetActive(false); // 점수 관리
        FeverManager.SetActive(false); // 피버매니저

        s_GameOverUI.SetActive(true); // 게임오버 UI

        ClearAllMonsters();
        ClearAllItems();
        ClearAllEnemyBullets();
    }

private void ClearAllMonsters()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        foreach (Monster monster in monsters)
        {
            Destroy(monster.gameObject);
        }
    }

    private void ClearAllItems()
    {
        c_BonusItem[] item = FindObjectsByType<c_BonusItem>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        foreach (c_BonusItem items in item)
        {
            Destroy(items.gameObject);
        }
    }

    private void ClearAllEnemyBullets()
    {
        h_Inkball[] inkBalls = FindObjectsByType<h_Inkball>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (h_Inkball inkBall in inkBalls)
        {
            Destroy(inkBall.gameObject);
        }
    }

    private void HpUISetting()
    {
        if (s_PlayerController.c_hp == 3)
        {
            s_Heart1.SetActive(true);
            s_Heart2.SetActive(true);
            s_Heart3.SetActive(true);
        }
        if (s_PlayerController.c_hp == 2)
        {
            s_Heart1.SetActive(true);
            s_Heart2.SetActive(true);
            s_Heart3.SetActive(false);
        }
        if (s_PlayerController.c_hp == 1)
        {
            s_Heart1.SetActive(true);
            s_Heart2.SetActive(false);
            s_Heart3.SetActive(false);
        }
    }


}