using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("점수 설정")]
    [SerializeField] private float score = 0f;

    [Header("참조")]
    [SerializeField] private BackGround background;

    private void OnEnable()
    {
        GameStart();
    }

    public void GameStart()
    {
        score = 0f;
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
        }

    }
}