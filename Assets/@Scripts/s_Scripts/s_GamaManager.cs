using UnityEngine;

public class s_GamaManager : MonoBehaviour
{
    public static s_GamaManager s_instance; // 메모리에 할당
    public s_PlayerInfo s_Player;

    // TODO : 골드메탈 무한맵 이동방식으로 구현하다가 실패
    // 실패사유 > 입맛에 따라 타일맵을 이동(마인크래프트 청크느낌?) 하려했는데 어..음...흠..

    private void Awake()
    {
        s_instance = this;
    }


}
