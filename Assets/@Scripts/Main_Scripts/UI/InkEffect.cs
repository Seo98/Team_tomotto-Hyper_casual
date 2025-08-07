using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InkEffect : MonoBehaviour
{
    // Dev_H : 화면을 가득 채운 검은 UI 이미지가 투명한 상태로 켜져있다가,
    //         잉크볼을 맞으면 알파값을 최대로 올려서 화면을 가린 후 차차 다시 투명해지게 작동합니다.
    // Dev_H : 나중에 전용 이미지 (먹물 얼룩 이미지) 만들어서 교체하면 좋을 듯 합니다.

    // Dev_S : 시웅님 라인
    public Image overlayImage;
    public float fadeOutTime = 3f; // Dev_H : 3초 동안 돌아오도록

    FeverTimeManager fManager;
    private void Start()
    {
        fManager = FindFirstObjectByType<FeverTimeManager>();
    }

    public void PlayEffect()
    {
        if(fManager.isFever)
            return;

        StopAllCoroutines();
        SetAlpha(1f);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f); // Dev_H : 0.5초 기다리고 돌아옴

        float timer = 0f;
        float percent = 0f;

        while (percent < 1f)
        {
            timer += Time.deltaTime;
            percent = timer / fadeOutTime;

            float alpha = Mathf.Lerp(1f, 0f, percent);
            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);
    }

    public void SetAlpha(float alpha) // Dev_s : 참조를 위한 public 변경
    {
        Color color = overlayImage.color;
        color.a = alpha;
        overlayImage.color = color;
    }
}