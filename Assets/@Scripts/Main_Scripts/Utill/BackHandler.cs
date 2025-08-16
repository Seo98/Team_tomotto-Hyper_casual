using UnityEngine;

public class SimpleBackHandler : MonoBehaviour
{
    void Update()
    {
        // ESC 키나 안드로이드 백버튼 감지
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackButton();
        }
    }

    void HandleBackButton()
    {
        // 현재 상황에 따른 간단한 처리
        if (Time.timeScale == 0) // 이미 일시정지 상태면
        {
            ResumeGame();
        }
        else // 게임 중이면
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        // 일시정지 UI 표시
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        // 일시정지 UI 숨김
    }
}