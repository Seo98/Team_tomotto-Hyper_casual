using UnityEngine;

public static class s_GridCoordinateConverter
{
    // 월드 좌표를 그리드 좌표로 변환
    public static Vector2Int GetGridCoordinates(Vector3 worldPosition, float gridSize)
    {
        // Y축은 아래로 진행하므로, Y좌표는 음수일수록 더 큰 그리드 인덱스를 갖도록 조정
        // 그리드 중앙을 기준으로 계산하기 위해 반올림
        int gridX = Mathf.RoundToInt(worldPosition.x / gridSize);
        int gridY = Mathf.RoundToInt(worldPosition.y / gridSize);
        return new Vector2Int(gridX, gridY);
    }

    // 그리드 좌표를 월드 좌표로 변환
    public static Vector3 GetWorldPosition(Vector2Int gridCoordinates, float gridSize)
    {
        // 그리드 중앙에 위치하도록 계산
        float worldX = gridCoordinates.x * gridSize;
        float worldY = gridCoordinates.y * gridSize;
        return new Vector3(worldX, worldY, 0); // 2D 게임이므로 Z는 0
    }
}
