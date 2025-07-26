using UnityEngine;
using UnityEngine.UI;

public class Pause_UI : MonoBehaviour
{
    public static bool s_isPaused = false;
    public GameObject s_pauseMenuUI;
    public Button pauseButton;
    public Button resumeButton;



    private void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);

    }

    void Update()
    {

    }

    void Resume()
    {
        s_pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        s_isPaused = false;
    }

    void Pause()
    {
        s_pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        s_isPaused = true;
    }
}
