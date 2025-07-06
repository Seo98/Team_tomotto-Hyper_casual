using System.Collections.Generic;
using UnityEngine;

public class s_GridObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] s_GridPrefabs; // 풀링할 그리드 프리팹들

    // 각 프리팹 타입별로 풀을 관리
    private Dictionary<GameObject, Queue<GameObject>> s_PoolDictionary = new Dictionary<GameObject, Queue<GameObject>>();
    private Transform s_PoolParent; // 생성된 오브젝트들을 담을 부모 Transform

    void Awake()
    {
        s_PoolParent = new GameObject("GridPool").transform;
        s_PoolParent.SetParent(transform); // MapManager의 자식으로 설정

        // 초기 풀 생성 (선택 사항, 필요에 따라 미리 생성 가능)
        foreach (GameObject prefab in s_GridPrefabs)
        {
            s_PoolDictionary.Add(prefab, new Queue<GameObject>());
        }
    }

    // 풀에서 그리드 오브젝트 가져오기
    public GameObject GetGrid(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!s_PoolDictionary.ContainsKey(prefab))
        {
            Debug.LogError($"Pool for prefab {prefab.name} does not exist.");
            return null;
        }

        GameObject objectToSpawn;

        if (s_PoolDictionary[prefab].Count > 0)
        {
            objectToSpawn = s_PoolDictionary[prefab].Dequeue();
            objectToSpawn.SetActive(true);
        }
        else
        {
            objectToSpawn = Instantiate(prefab, s_PoolParent);
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    // 그리드 오브젝트를 풀에 반환
    public void ReturnGrid(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        // 어떤 프리팹에서 생성된 오브젝트인지 알 수 없으므로, 여기서는 단순히 비활성화만 하고 풀에 다시 넣지는 않습니다.
        // 만약 풀에 다시 넣으려면, 오브젝트 생성 시 어떤 프리팹에서 왔는지 정보를 저장해야 합니다.
        // 현재는 단순히 비활성화하여 재활용하는 방식으로 구현합니다.
        // TODO: 오브젝트가 어떤 프리팹에서 왔는지 추적하여 해당 풀에 반환하는 로직 추가
    }

    // 모든 활성화된 그리드를 비활성화하고 풀에 반환 (씬 전환 등)
    public void ReturnAllActiveGrids()
    {
        // 이 풀에서 생성된 모든 활성화된 그리드를 찾아 비활성화
        foreach (Transform child in s_PoolParent)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
                // TODO: 어떤 프리팹에서 왔는지 추적하여 해당 풀에 반환하는 로직 추가
            }
        }
    }

    // 특정 그리드 프리팹을 가져올 수 있도록 public으로 노출
    public GameObject GetRandomGridPrefab()
    {
        if (s_GridPrefabs == null || s_GridPrefabs.Length == 0)
        {
            Debug.LogError("s_GridPrefabs에 할당된 프리팹이 없습니다.");
            return null;
        }
        return s_GridPrefabs[Random.Range(0, s_GridPrefabs.Length)];
    }
}
