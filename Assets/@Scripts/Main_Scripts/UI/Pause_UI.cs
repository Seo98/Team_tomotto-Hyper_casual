using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause_UI : MonoBehaviour
{
    public static bool s_isPaused = false;
    public GameObject s_pauseMenuUI;
    public Button pauseButton;
    public Button resumeButton;
    public Button homeButton;

    public float countdownDuration = 3f;
    public TextMeshProUGUI countdownText; // UI Text 오브젝트 할당

    public SoundManager soundManager;
    public UIManager uiManger;

    public GameObject[] Managers;
    public GameObject[] userInterface;
    public GameObject mainGame;


    private void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(StartMyCoroutine);
        homeButton.onClick.AddListener(GoHome);
    }

    public void StartMyCoroutine()
    {
        StartCoroutine(Resume());
    }

    void Update()
    {

    }

    IEnumerator Resume()
    {
        if (s_isPaused)
        {
            s_pauseMenuUI.SetActive(false);

            Transform first = countdownText.transform.GetChild(0);
            first.gameObject.SetActive(true);

            for (float i = countdownDuration; i > 0; i--)
            {
                countdownText.text = Mathf.CeilToInt(i).ToString(); // UI 텍스트 업데이트
                yield return new WaitForSecondsRealtime(1f); // 1초 대기
            }
            // 3초동안 멈추는 기능

            countdownText.text = ""; // 3초후 텍스트 초기화

            first.gameObject.SetActive(false); // 자식 이미지 꺼버림



            // 다시시작
            Time.timeScale = 1f;
            s_isPaused = false;
            soundManager.BgmSoundsResume();
        }
    }

    void Pause()
    {
        if (!s_isPaused)
        {
            s_pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            s_isPaused = true;
            soundManager.BgmSoundsPause();
        }
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        s_isPaused = false;


        Managers[0].SetActive(false);
        Managers[1].SetActive(false);
        //
        userInterface[0].SetActive(true);
        userInterface[1].SetActive(false);
        userInterface[2].SetActive(false);
        userInterface[3].SetActive(false);
        userInterface[4].SetActive(false);
        userInterface[5].SetActive(false);
        userInterface[6].SetActive(false);
        userInterface[7].SetActive(false);

        //
        mainGame.SetActive(false);

        uiManger.ClearAllMonsters();
        uiManger.ClearAllItems();
        uiManger.ClearAllEnemyBullets();

        soundManager.BgmSoundStop();
        soundManager.BgmSoundPlay("Gb 1");

        //

    }
}
