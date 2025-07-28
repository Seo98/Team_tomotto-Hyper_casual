using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FeverTimeManager : MonoBehaviour
{
    // dev_s: 여기 은주님라인, 무브스피드 계수 건든거 말고는 손본거 없는걸로 기억합니다.
    PlayerController player;

    Collider2D playColl;
    public Image feverImage;
    public GameObject feverStartImage;
    public bool isFever;
    public float nowGauge;

    private void OnEnable()
    {
        feverImage.fillAmount = 0;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playColl = player.GetComponent<Collider2D>();

    }
    private void Update()
    {
        while (feverImage.fillAmount < 1)
        {
            feverImage.fillAmount += Time.deltaTime * 0.06f;
            nowGauge = feverImage.fillAmount;
            break;
        }
        if (!isFever && feverImage.fillAmount >= 1)
            StartCoroutine(FeverTime());                    
    }

    IEnumerator FeverTime()
    {
        feverStartImage.SetActive(true);
        Debug.Log("IsFever");
         isFever = true;
         player.moveSpeed = 2f;
        
        yield return new WaitForSeconds(5f);

        isFever = false;
        player.moveSpeed = 0.2f;
        playColl.isTrigger = false;
        feverImage.fillAmount = 0f;
        feverStartImage.SetActive(false);
    }
}