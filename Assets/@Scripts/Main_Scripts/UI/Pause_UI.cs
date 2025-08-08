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

    public float countdownDuration = 3f;
    public TextMeshProUGUI countdownText; // UI Text 오브젝트 할당

    public SoundManager soundManager;
    private void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(StartMyCoroutine);

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
}
