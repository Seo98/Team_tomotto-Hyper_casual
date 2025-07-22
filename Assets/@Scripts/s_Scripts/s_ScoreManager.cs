using UnityEngine;
using TMPro;

public class s_ScoreManager : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI s_scoreText;

    [Header("점수 설정")]
    [SerializeField] private float s_scoremanage = 0f;  // 점수

    [Header("참조")]
    [SerializeField] private s_BackGround s_background;

    private void OnEnable()
    {
        GameStart();
    }

    public void GameStart()
    {
        // 게임 시작 시 초기화
        s_scoremanage = 0f; // 점수 초기화
        if (s_scoreText != null)
        {
            s_scoreText.text = "Score: 0"; // 초기 점수 표시
        }
    }

    void Update()
    {
        //Todo : 점수계산 로직추가

        // 임시 : 시간에 따라 점수 증가
        s_scoremanage += Time.deltaTime * 10f; // 시간에 따라 점수 증가

        if (s_scoreText != null)
        {
            s_scoreText.text = $"Score: {Mathf.FloorToInt(s_scoremanage)}"; // 점수 표시
        }

    }
}