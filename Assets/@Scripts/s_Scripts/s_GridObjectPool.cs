using System.Collections.Generic;
using UnityEngine;

public class s_GridObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] s_GridPrefabs; // 풀링할 그리드 프리팹들
    [SerializeField] private int initialPoolSize = 9; // 각 프리팹당 미리 생성할 오브젝트 개수

    private Dictionary<string, Queue<GameObject>> s_PoolDictionary = new Dictionary<string, Queue<GameObject>>();
    private Transform s_PoolParent; // 생성된 오브젝트들을 담을 부모 Transform

    void Awake()
    {
        s_PoolParent = new GameObject("GridPool").transform;
        s_PoolParent.SetParent(transform);

        foreach (GameObject prefab in s_GridPrefabs)
        {
            s_PoolDictionary.Add(prefab.name, new Queue<GameObject>());
            // 미리 오브젝트 생성하여 풀에 추가
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab, s_PoolParent);
                obj.SetActive(false); // 비활성화 상태로 풀에 보관

                // s_PooledGrid 컴포넌트 추가 및 원본 프리팹 정보 저장
                s_PooledGrid pooledGrid = obj.GetComponent<s_PooledGrid>();
                if (pooledGrid == null)
                {
                    pooledGrid = obj.AddComponent<s_PooledGrid>();
                }
                pooledGrid.s_OriginalPrefab = prefab;

                s_PoolDictionary[prefab.name].Enqueue(obj);
            }
        }
    }

    public GameObject GetGrid(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!s_PoolDictionary.ContainsKey(prefab.name))
        {
            Debug.LogError($"Pool for prefab {prefab.name} does not exist.");
            return null;
        }

        GameObject objectToSpawn;

        if (s_PoolDictionary[prefab.name].Count > 0)
        {
            objectToSpawn = s_PoolDictionary[prefab.name].Dequeue();
            objectToSpawn.SetActive(true);
        }
        else
        {
            // 풀이 비어있으면 새로 생성
            objectToSpawn = Instantiate(prefab, s_PoolParent);
            
            // 새로 생성된 오브젝트에 s_PooledGrid 컴포넌트를 붙여주고, 원본 프리팹 정보를 저장합니다.
            s_PooledGrid pooledGrid = objectToSpawn.GetComponent<s_PooledGrid>();
            if (pooledGrid == null)
            {
                pooledGrid = objectToSpawn.AddComponent<s_PooledGrid>();
                }
            pooledGrid.s_OriginalPrefab = prefab; 
        }

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public void ReturnGrid(GameObject objectToReturn)
    {
        s_PooledGrid pooledGrid = objectToReturn.GetComponent<s_PooledGrid>();

        if (pooledGrid == null || pooledGrid.s_OriginalPrefab == null)
        {
            Debug.LogError($"풀에 반환하려는 {objectToReturn.name}에 s_PooledGrid 정보가 없어 파괴합니다.");
            Destroy(objectToReturn);
            return;
        }

        string prefabName = pooledGrid.s_OriginalPrefab.name;
        if (s_PoolDictionary.ContainsKey(prefabName))
        {
            objectToReturn.SetActive(false);
            s_PoolDictionary[prefabName].Enqueue(objectToReturn);
        }
        else
        {
            Debug.LogWarning($"반환하려는 오브젝트({objectToReturn.name})에 해당하는 풀이 없습니다.");
            Destroy(objectToReturn);
        }
    }

    public void ReturnAllActiveGrids()
    {
        foreach (Transform child in s_PoolParent)
        {
            if (child.gameObject.activeSelf)
            {
                ReturnGrid(child.gameObject);
            }
        }
    }

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