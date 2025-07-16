using UnityEngine;
using UnityEngine.UI;

public class s_UIManager : MonoBehaviour
{
    [Header("게임매니저 접근")]
    public s_GameManager s_GameManager;


    [Header("UI 오브젝트(버튼)")]
    public Button s_Button;
    public Button s_RestratButton;

    [Header("UI 오브젝트(부모)")]
    public GameObject s_IntroObj;
    public GameObject s_StartGameUI;
    public GameObject s_StartGame;
    public GameObject s_GameOverUI;
    public GameObject s_ScoreManag;

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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            s_IntroObj.SetActive(false); // 인트로UI

            s_StartGameUI.SetActive(false); // 게임시작 UI
            s_StartGame.SetActive(false); // 게임시작
            s_ScoreManag.SetActive(false); // 점수 관리

            s_GameOverUI.SetActive(true); // 게임오버 UI
        }
    }
}
