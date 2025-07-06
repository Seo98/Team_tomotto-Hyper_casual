using UnityEngine;

public class s_CameraFollow : MonoBehaviour
{
    // 플레이어의 Transform 컴포넌트를 할당할 변수입니다.
    // Unity 에디터의 Inspector 창에서 직접 플레이어 오브젝트를 끌어다 놓으면 됩니다.
    public Transform playerTarget;

    // 카메라와 플레이어 사이의 초기 간격을 저장할 변수입니다.
    private Vector3 offset;

    void Start()
    {
        // 게임이 시작될 때, 카메라와 플레이어 사이의 거리를 계산하여 offset에 저장합니다.
        // 이렇게 하면 씬에 배치한 카메라의 초기 Z축 거리와 각도가 그대로 유지됩니다.
        if (playerTarget != null)
        { 
            offset = transform.position - playerTarget.position;
        }
    }

    void LateUpdate()
    {
        // LateUpdate는 모든 Update 함수가 실행된 후에 호출됩니다.
        // 플레이어가 움직임을 마친 후에 카메라가 위치를 업데이트하므로, 카메라 떨림 현상을 방지할 수 있습니다.
        if (playerTarget != null)
        { 
            // 플레이어의 현재 위치에 저장해둔 offset 값을 더해 카메라의 새 위치를 결정합니다.
            Vector3 newPosition = playerTarget.position + offset;
            transform.position = newPosition;
        }
    }
}