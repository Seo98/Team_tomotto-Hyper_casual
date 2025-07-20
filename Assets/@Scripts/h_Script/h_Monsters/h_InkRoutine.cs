using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class h_InkRoutine : MonoBehaviour
{
    public Image fadePanel; // 페이드에 활용될 UI 인스펙터

    public void OnFade(float fadeTime, Color color, bool isFadeStart)       // 페이드를 시작하는 메서드 
    {
        StartCoroutine(Fade(fadeTime, color, isFadeStart));                 // Fade() 코루틴을 실행함, fadeTime = 페이드 시간, color = 페이드의 색상 값, isFadeStart = 페이드 실행 여부를 감지하는 bool값
    }

    public IEnumerator Fade(float fadeTime, Color color, bool isFadeStart)  // Fade 코루틴
    {
        float timer = 0f;       // 경과 시간 초기화
        float percent = 0f;     // Fade 퍼센트 초기화
        while (percent < 1f)    // 퍼센트가 완료(1)이 될 때까지 반복
        {
            timer += Time.deltaTime;    // 타이머에 1씩 추가. Time.deltaTime은 이전 프레임에서 지금까지 걸린 시간(초)
            percent = timer / fadeTime; // 퍼센트를 페이드 시간 분에 경과 시간으로 설정,

            float value = isFadeStart ? percent : 1 - percent;  // 삼항 연산자로 페이드 방향 결정, 참 = 퍼센트 0 -> 1 점점 어두워짐, 거짓 = 퍼센트 1 -> 0 점점 보임

            fadePanel.color = new Color(color.r, color.g, color.b, value);  // fadePanel의 색상 알파값을 바꿈
            yield return null;  // 부드러운 전환을 위해 매 프레임마다 잠시 멈춤
        }
    }
}
