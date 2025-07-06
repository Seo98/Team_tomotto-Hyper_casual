using System.Collections.Generic;
using UnityEngine;

public class s_TilemapReposition : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private float s_GridSize = 20f; // 각 그리드 조각의 크기 (가로/세로)
    [SerializeField] private int s_ViewDistance = 3; // 플레이어 주변 몇 칸까지 그리드를 생성하고 유지할지 (그리드 단위)

    [Header("Player Settings")]
    [SerializeField] private Transform s_PlayerTransform; // 플레이어 Transform

    [Header("Dependencies")]
    [SerializeField] private s_GridObjectPool s_GridPool; // 그리드 오브젝트 풀 참조

    // 현재 활성화된 그리드들을 그리드 좌표와 GameObject로 매핑하여 관리
    private Dictionary<Vector2Int, GameObject> s_ActiveGrids = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int s_LastPlayerGridPos; // 플레이어가 마지막으로 위치했던 그리드 좌표

    void Start()
    {
        // 플레이어 Transform이 할당되었는지 확인
        if (s_PlayerTransform == null)
        {
            Debug.LogError("s_PlayerTransform이 할당되지 않았습니다. 플레이어 오브젝트를 Inspector에 드래그해주세요.");
            return;
        }
        // 그리드 풀이 할당되었는지 확인
        if (s_GridPool == null)
        {
            Debug.LogError("s_GridPool이 할당되지 않았습니다. Inspector에 s_GridObjectPool 컴포넌트를 드래그해주세요.");
            return;
        }

        // 초기 그리드 생성
        s_LastPlayerGridPos = s_GridCoordinateConverter.GetGridCoordinates(s_PlayerTransform.position, s_GridSize);
        GenerateGridsAroundPlayer(s_LastPlayerGridPos);
    }

    void Update()
    {
        Vector2Int currentPlayerGridPos = s_GridCoordinateConverter.GetGridCoordinates(s_PlayerTransform.position, s_GridSize);

        // 플레이어의 그리드 위치가 변경되면 새로운 그리드 생성 및 오래된 그리드 제거
        if (currentPlayerGridPos != s_LastPlayerGridPos)
        {
            s_LastPlayerGridPos = currentPlayerGridPos;
            GenerateGridsAroundPlayer(currentPlayerGridPos);
            RemoveDistantGrids(currentPlayerGridPos);
        }
    }

    // 플레이어 주변에 그리드 생성
    private void GenerateGridsAroundPlayer(Vector2Int playerGridPos)
    {
        for (int x = playerGridPos.x - s_ViewDistance; x <= playerGridPos.x + s_ViewDistance; x++)
        {
            for (int y = playerGridPos.y - s_ViewDistance; y <= playerGridPos.y + s_ViewDistance; y++)
            {
                Vector2Int currentGridCoords = new Vector2Int(x, y);

                // 이미 생성된 그리드인지 확인
                if (!s_ActiveGrids.ContainsKey(currentGridCoords))
                {
                    SpawnNewGrid(currentGridCoords);
                }
            }
        }
    }

    // 특정 그리드 좌표에 새로운 그리드 생성
    private void SpawnNewGrid(Vector2Int gridCoords)
    {
        GameObject selectedGridPrefab = s_GridPool.GetRandomGridPrefab();
        if (selectedGridPrefab == null) return;

        // 그리드 좌표에 해당하는 월드 위치 계산
        Vector3 spawnPosition = s_GridCoordinateConverter.GetWorldPosition(gridCoords, s_GridSize);

        // 그리드 풀에서 오브젝트 가져오기
        GameObject newGrid = s_GridPool.GetGrid(selectedGridPrefab, spawnPosition, Quaternion.identity);
        s_ActiveGrids.Add(gridCoords, newGrid);
    }

    // 플레이어로부터 멀리 떨어진 그리드 제거
    private void RemoveDistantGrids(Vector2Int currentPlayerGridPos)
    {
        List<Vector2Int> gridsToRemove = new List<Vector2Int>();

        foreach (var entry in s_ActiveGrids)
        {
            Vector2Int gridCoords = entry.Key;
            // 맨하탄 거리 (Manhattan distance)를 사용하여 거리 계산
            if (Mathf.Abs(gridCoords.x - currentPlayerGridPos.x) > s_ViewDistance || 
                Mathf.Abs(gridCoords.y - currentPlayerGridPos.y) > s_ViewDistance)
            {
                gridsToRemove.Add(gridCoords);
            }
        }

        foreach (Vector2Int gridCoords in gridsToRemove)
        {
            GameObject gridObject = s_ActiveGrids[gridCoords];
            s_ActiveGrids.Remove(gridCoords);
            s_GridPool.ReturnGrid(gridObject); // 풀에 반환
        }
    }
}