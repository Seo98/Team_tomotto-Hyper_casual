using UnityEngine;

public class s_GameManager : MonoBehaviour
{
    public static s_GameManager s_instance; // 다른 스크립트에서 접근할 수 있는 싱글톤 인스턴스

    [HideInInspector]
    public s_PlayerInfo s_playerInfoRef; // 플레이어 정보(PlayerInfo)에 대한 참조

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_playerInfoRef = FindFirstObjectByType<s_PlayerInfo>();

        if (s_playerInfoRef == null)
        {
            Debug.LogError("정보못찾음 GameManager.cs 26 < ");
        }
    }
}