using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI currentScoreUI;
    public TextMeshProUGUI bestScoreUI;

    public int currentScore;
    public int bestScore;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);     // �ʱ� �ְ���

        bestScoreUI.text = "Bset Score  :  " + bestScore;   // �ְ��� UI�� �ҷ�����
    }

    public void SetScore(int value)
    {
        currentScore = value;

        currentScoreUI.text = "Curr Score : " + currentScore;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            bestScoreUI.text = "Best Score : " + bestScore;

            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
}
