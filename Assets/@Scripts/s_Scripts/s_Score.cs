using UnityEngine;
using TMPro;

public class s_Score : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private TextMeshProUGUI s_scoreText;

    [Header("점수 설정")]
    [SerializeField] private float s_scoreMultiplier = 1f;

    private Transform s_playerTransform;
    private float s_startYPosition;

    void Start()
    {
        GameStart();
    }

    private void OnEnable()
    {
        GameStart();
    }

    public void GameStart()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            s_playerTransform = player.transform;
            s_startYPosition = s_playerTransform.position.y;
        }
        else
        {
            Debug.LogWarning("Player 태그x");
        }
    }

    void Update()
    {
        if (s_playerTransform == null) return;

        float distance = s_startYPosition - s_playerTransform.position.y;

        if (distance > 0)
        {
            int score = (int)(distance * s_scoreMultiplier);
            if (s_scoreText != null)
                s_scoreText.text = $"Score: {score}";
        }
    }
}