using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    [Header("경험치바 UI")]
    public Image expBarImage; 

    private void Update()
    {
        // 싱글톤으로 바로 접근해서 경험치바 업데이트
        if (LevelUpManager.Instance != null && expBarImage != null)
        {
            float expRatio = LevelUpManager.Instance.curExp / LevelUpManager.Instance.maxExp;
            expBarImage.fillAmount = expRatio;
        }
    }

    
    public void InitializeExpBar()
    {
        if (expBarImage != null)
        {
            expBarImage.fillAmount = 0f;
        }
    }

    
    public static void InitializeExpBarStatic()
    {
        ExpBarUI expBarUI = FindFirstObjectByType<ExpBarUI>();
        if (expBarUI != null)
        {
            expBarUI.InitializeExpBar();
        }
    }
}