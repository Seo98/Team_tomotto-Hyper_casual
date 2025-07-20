using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class h_InkRoutine : MonoBehaviour
{
    public Image fadePanel; // ���̵忡 Ȱ��� UI �ν�����

    public void OnFade(float fadeTime, Color color, bool isFadeStart)       // ���̵带 �����ϴ� �޼��� 
    {
        StartCoroutine(Fade(fadeTime, color, isFadeStart));                 // Fade() �ڷ�ƾ�� ������, fadeTime = ���̵� �ð�, color = ���̵��� ���� ��, isFadeStart = ���̵� ���� ���θ� �����ϴ� bool��
    }

    public IEnumerator Fade(float fadeTime, Color color, bool isFadeStart)  // Fade �ڷ�ƾ
    {
        float timer = 0f;       // ��� �ð� �ʱ�ȭ
        float percent = 0f;     // Fade �ۼ�Ʈ �ʱ�ȭ
        while (percent < 1f)    // �ۼ�Ʈ�� �Ϸ�(1)�� �� ������ �ݺ�
        {
            timer += Time.deltaTime;    // Ÿ�̸ӿ� 1�� �߰�. Time.deltaTime�� ���� �����ӿ��� ���ݱ��� �ɸ� �ð�(��)
            percent = timer / fadeTime; // �ۼ�Ʈ�� ���̵� �ð� �п� ��� �ð����� ����,

            float value = isFadeStart ? percent : 1 - percent;  // ���� �����ڷ� ���̵� ���� ����, �� = �ۼ�Ʈ 0 -> 1 ���� ��ο���, ���� = �ۼ�Ʈ 1 -> 0 ���� ����

            fadePanel.color = new Color(color.r, color.g, color.b, value);  // fadePanel�� ���� ���İ��� �ٲ�
            yield return null;  // �ε巯�� ��ȯ�� ���� �� �����Ӹ��� ��� ����
        }
    }
}
