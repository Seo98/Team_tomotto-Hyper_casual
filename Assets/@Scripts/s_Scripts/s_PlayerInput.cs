using UnityEngine;

public class s_PlayerInput : MonoBehaviour
{
    public Vector2 s_MoveDirection { get; private set; } // 방향

    void Update()
    {
        // 매 프레임마다 입력 처리 함수 호출
        ProcessInput();
    }

    //FIXME : 실제 빌드시 버튼 컴포넌트에 연결필요 또는 다른방식으로 해야함
    // 추후키보드 입력방식은 제거 필요
    private void ProcessInput() // 입력시(ad /방향키좌우)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        // 플레이어가 수평입력을 하지 않아도 direction 벡터는 (0,-1)값이 나와요 그래서 지절로움직여용
        // 그리고 수평입력은 받지않습니당
        Vector2 direction = new Vector2(moveHorizontal, -1);

        //정규화
        s_MoveDirection = direction.normalized;
    }
}

