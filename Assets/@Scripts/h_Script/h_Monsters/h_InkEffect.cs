using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class h_InkEffect : MonoBehaviour
{
    public Image overlayImage;
    public float fadeOutTime = 3f;

    public void PlayEffect()
    {
        StopAllCoroutines(); // ���� �� �¾Ƶ� ��ø���� �ʰ�
        SetAlpha(1f);        // ��� �˰� ������
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
