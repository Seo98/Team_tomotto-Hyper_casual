using UnityEngine;
using UnityEngine.UI;

public class SimpleResolution : MonoBehaviour
{
    public CanvasScaler thisCanvas;

    void Start()
    {
        // Canvas Scaler가 없으면 자동으로 찾기
        if (thisCanvas == null)
        {
            thisCanvas = FindFirstObjectByType<CanvasScaler>();
        }

        SetResolution();
    }

    void SetResolution()
    {
        // 기본 해상도 비율 (9:16)
        float fixedAspectRatio = 9f / 16f;

        // 현재 해상도의 비율
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        // 현재 해상도 가로 비율이 더 길 경우
        if (currentAspectRatio > fixedAspectRatio)
        {
            thisCanvas.matchWidthOrHeight = 1;
        }
        // 현재 해상도의 세로 비율이 더 길 경우
        else if (currentAspectRatio < fixedAspectRatio)
        {
            thisCanvas.matchWidthOrHeight = 0;
        }

        Debug.Log($"해상도: {Screen.width}x{Screen.height}, 비율: {currentAspectRatio:F2}, 설정: {thisCanvas.matchWidthOrHeight}");
    }
}