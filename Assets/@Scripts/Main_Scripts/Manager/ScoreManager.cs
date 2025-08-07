using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [Header("점수 설정")]
    [SerializeField] private float score = 0f;
    [SerializeField] private int bestScore = 0;

    [Header("참조")]
    [SerializeField] private BackGround background;

    private void OnEnable()
    {
        GameStart();
    }

    public void GameStart()
    {
        score = 0f;

        bestScore = PlayerPrefs.GetInt("BestScore", 0);     // Dev_H: 초기 최고점
        bestScoreText.text = "Bset Score  :  " + bestScore; // Dev_H: 최고점 UI에 불러오기

        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }

    void Update()   
    {
        score += Time.deltaTime * 10f; // Dev_s : 여기가 점수 계산 로직인데 여기서 몬스터쪽 참조해서 여기다 로직짜면 될듯?합니다.
                                       // 간단하게는 몬스터 사망시 여기다 점수 ++ 해도될듯

        if (scoreText != null)
        {
            scoreText.text = $"Score: {Mathf.FloorToInt(score)}";

            if (score > bestScore)
            {
                bestScore = (int)score; // Dev_H: 베스트 스코어는 int값으로 변형되어 저장되게
                bestScoreText.text = "Best Score : " + bestScore;

                PlayerPrefs.SetInt("BestScore", bestScore);
            }
        }
    }
}