using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    // Dev_H : 몬스터들이 무작위 값 만큼 상하or좌우로 움직이게 하도록 만들었습니다.

    public enum MoveType { Horizontal, Vertical }
    public MoveType mmMoveType;

    public float mmTheta;     // 각도, Mathf.Sin에 대입해서 부드러운 움직임을 주기 위해 활용
    public float mmPower;     // 폭 (얼마나 강하게 흔들릴지)
    public float mmSpeed;     // 속도

    private Vector3 mmInitPos;    // initPos 변수 내에 Vector3 기반 위치값을 저장해 놓음

    private void Awake()
    {
        mmPower = Random.Range(-1f, 1f);
        mmSpeed = Random.Range(-1f, 1f);
    }

    void Start()
    {
        mmInitPos = transform.position;
    }

    void Update()
    {
        mmTheta += Time.deltaTime * mmSpeed;

        Vector3 pos = transform.position;

        if (mmMoveType == MoveType.Horizontal)
        {
            pos.x = mmInitPos.x + mmPower * Mathf.Sin(mmTheta); // 좌우 흔들림
        }
        else if (mmMoveType == MoveType.Vertical)
        {
            pos.y += mmPower * Mathf.Sin(mmTheta) * Time.deltaTime; // 세로 흔들림
        }

        transform.position = pos;
    }
}
