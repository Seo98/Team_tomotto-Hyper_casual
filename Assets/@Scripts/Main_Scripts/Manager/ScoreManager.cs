using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText2;

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

        bestScore = PlayerPrefs.GetInt("최고 점수", 0);     // Dev_H: 초기 최고점
        bestScoreText.text = "최고 점수 : " + bestScore; // Dev_H: 최고점 UI에 불러오기

        if (scoreText != null)
        {
            scoreText.text = "0";
        }
    }

    void Update()   
    {
        score += Time.deltaTime * 10f; // Dev_s : 여기가 점수 계산 로직인데 여기서 몬스터쪽 참조해서 여기다 로직짜면 될듯?합니다.
                                       // 간단하게는 몬스터 사망시 여기다 점수 ++ 해도될듯

        if (scoreText != null)
        {
            scoreText.text = $"점수 : {Mathf.FloorToInt(score)}";

            if (score > bestScore)
            {
                bestScore = (int)score; // Dev_H: 베스트 스코어는 int값으로 변형되어 저장되게
                bestScoreText.text = "최고 점수 : " + bestScore;

                PlayerPrefs.SetInt("최고 점수", bestScore);
            }
        }
    }
    public void IntroScore() //dev_c : 인트로창에 표시할 스코어 함수
    {
        //PlayerPrefs.SetInt("최고 점수", bestScore);
        bestScoreText2.text = $"{bestScore}";//Dev_c : 인트로 스코어창에 표시될 스코어       
    }
}