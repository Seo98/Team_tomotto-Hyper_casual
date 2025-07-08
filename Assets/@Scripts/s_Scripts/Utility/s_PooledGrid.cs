using UnityEngine;

// 이 스크립트는 풀링되는 그리드 오브젝트에 붙여서
// 자신이 어떤 원본 프리팹으로부터 생성되었는지 기억하는 역할을 합니다.
public class s_PooledGrid : MonoBehaviour
{
    // public으로 선언하여 s_GridObjectPool 스크립트에서 이 변수에 접근할 수 있습니다.
    public GameObject s_OriginalPrefab;
}
