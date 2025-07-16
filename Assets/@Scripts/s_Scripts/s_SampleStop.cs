using UnityEngine;

public class s_SampleStop : MonoBehaviour
{
    public static bool s_isPaused = false;
    public GameObject s_pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (s_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
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
