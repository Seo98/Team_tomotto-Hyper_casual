using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class c_FeverTimeManage : MonoBehaviour
{
    s_PlayerController player;
    s_PlayerInfo playerInfo;

    Collider2D c_playColl;
    public Image c_feverImage;
    public bool c_isFever;

    private void Awake()
    {
        c_feverImage.fillAmount = 0;
        player = GameObject.FindWithTag("Player").GetComponent<s_PlayerController>();

        c_playColl = player.GetComponent<Collider2D>();
        playerInfo = GameObject.FindWithTag("Player").GetComponent<s_PlayerInfo>();
    }
    private void Update()
    { 
        while (c_feverImage.fillAmount < 1)
        {
            c_feverImage.fillAmount += Time.deltaTime * 0.06f;

            break;
        }

        if (!c_isFever && c_feverImage.fillAmount >= 1)
            StartCoroutine(c_FeverTime());                    
    }

    IEnumerator c_FeverTime()
    {
         c_isFever = true;       
         c_playColl.isTrigger = true;
         playerInfo.s_moveSpeed = 15f;

        yield return new WaitForSeconds(5f);

        c_isFever = false;
        playerInfo.s_moveSpeed = 5f;
        c_feverImage.fillAmount = 0f;
    }
}
