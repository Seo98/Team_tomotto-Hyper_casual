using UnityEngine;

public class SpawnerMove : MonoBehaviour
{
    // Dev_H : 몬스터 스포너들이 상하or좌우로 움직이게 하도록 만들었습니다.
    //         현재 몬스터들이 스포너의 자식으로 소환되니 같이 상하좌우로 움직여서 느낌이 좋습니다.

    public enum MoveType { Horizontal, Vertical }
    public MoveType moveType;

    public float theta;         // 각도, Mathf.Sin에 대입해서 부드러운 움직임을 주기 위해 활용
    public float power = 0.1f;  // 폭 (얼마나 강하게 흔들릴지)
    public float speed = 1f;    // 속도

    private Vector3 initPos;    // initPos 변수 내에 Vector3 기반 위치값을 저장해 놓음

    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        theta += Time.deltaTime * speed;

        if (moveType == MoveType.Horizontal) 
            transform.position = new Vector3(initPos.x + power * Mathf.Sin(theta), initPos.y, initPos.z);

        else if (moveType == MoveType.Vertical)
            transform.position = new Vector3(initPos.x, initPos.y + power * Mathf.Sin(theta), initPos.z);
    }
}
