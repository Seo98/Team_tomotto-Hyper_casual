using UnityEngine;

public class EXPManager : MonoBehaviour
{
    public static EXPManager Instance { get; set; }

    // Dev_H: 현재 경혐치와 레벨, 최대경험치
    public int curExp = 0;
    public int curLevel = 1;
    public float maxExp = 100;  

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Dev_H: Monster 스크립트에서 GiveExp 함수에서 기능
    public void AddExp(int expAmount)
    {
        curExp += expAmount;
        Debug.Log($"Exp +{expAmount}, 현재: {curExp}");

        if (curExp >= maxExp)
        {
            LevelUp();  
        }
    }

    // Dev_H: 레벨업시 능력치 상승은 아직 미구현 
    private void LevelUp()
    {
        curLevel++;
        curExp = 0;
        maxExp += maxExp / 2.5f; ; // 점점 더 많은 경험치 필요하도록 증가

        Debug.Log($"Level Up! 현재 레벨: {curLevel}");
    }
}
