using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class h_InkEffect : MonoBehaviour
{
    public Image overlayImage;
    public float fadeOutTime = 3f;

    public void PlayEffect()
    {
        StopAllCoroutines(); // 여러 번 맞아도 중첩되지 않게
        SetAlpha(1f);        // 즉시 검게 가려짐
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);

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

    private void SetAlpha(float alpha)
    {
        Color color = overlayImage.color;
        color.a = alpha;
        overlayImage.color = color;
    }
}
