using UnityEngine;

public class s_GamaManager : MonoBehaviour
{
    public static s_GamaManager s_instance; //  Gama < 오타인거 알고있어용 ㅠ

    [Header("Player References")]
    public s_PlayerInfo s_PlayerInfoRef; // 플레이어 정보 스크립트 참조
    public s_PlayerMove s_PlayerMoveRef; // 플레이어 이동 스크립트 참조
    public s_PlayerInput s_PlayerInputRef; // 플레이어 입력 스크립트 참조

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // s_PlayerMove 스크립트 초기화
        if (s_PlayerMoveRef != null && s_PlayerInfoRef != null && s_PlayerInputRef != null)
        {
            s_PlayerMoveRef.Setup(s_PlayerInfoRef, s_PlayerInputRef);
        }
        else
        {
            Debug.LogError("s_GamaManager: 플레이어 관련 참조가 할당되지 않았습니다. Inspector를 확인해주세요.");
        }
    }
}