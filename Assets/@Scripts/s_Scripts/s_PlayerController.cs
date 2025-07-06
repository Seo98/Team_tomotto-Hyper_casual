using UnityEngine;

// 이 스크립트를 추가하면 아래 명시된 컴포넌트들이 자동으로 추가됩니다.
[RequireComponent(typeof(s_PlayerInfo))]
[RequireComponent(typeof(s_PlayerInput))]
[RequireComponent(typeof(s_PlayerMove))]
public class s_PlayerController : MonoBehaviour
{
    void Awake()
    {
        // 각 기능 컴포넌트들을 찾습니다.
        s_PlayerInfo playerInfo = GetComponent<s_PlayerInfo>();
        s_PlayerInput playerInput = GetComponent<s_PlayerInput>();
        s_PlayerMove playerMove = GetComponent<s_PlayerMove>();

        // s_PlayerMove 스크립트에 필요한 정보(Info, Input)를 전달하여 초기화합니다.
        playerMove.Setup(playerInfo, playerInput);
    }
}
