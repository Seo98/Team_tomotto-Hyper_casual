using UnityEngine;

public class s_GameManager : MonoBehaviour
{
    public static s_GameManager s_instance; // 싱글톤 인스턴스
    public s_PlayerInfo s_playerInfoRef;

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        GameStart();
    }


    public void GameStart()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            s_playerInfoRef = player.GetComponent<s_PlayerInfo>(); 
        }
        else
        {
            Debug.Log("#엄준식");
        }
    }
}
