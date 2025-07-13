using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class c_CannonfireManager : MonoBehaviour
{
    public Transform c_firePos;
    public GameObject c_ballPrefab;
    public Button c_fireButton;

    public Image buttonImage;

    public float c_shootPower = 20f;

    private void Awake()
    { 
        c_fireButton.onClick.AddListener(CannonFire);
    }

    void CannonFire()
    {
        if (buttonImage.fillAmount >= 1f)
        {
            C_AttackRoutine();
            StartCoroutine(C_buttonActive());
        }
    }

    public void C_AttackRoutine()
    {     
        GameObject c_fireball = Instantiate(c_ballPrefab, c_firePos);
        Rigidbody2D rb = c_fireball.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {   
            rb.AddForce(Vector2.down * c_shootPower, ForceMode2D.Impulse);            
        }    
    }

    IEnumerator C_buttonActive()
    {
        buttonImage.fillAmount = 0f;
        buttonImage.raycastTarget = false;

        while(buttonImage.fillAmount < 1)
        {
            buttonImage.fillAmount += Time.deltaTime * 0.2f;     

            yield return null;
        }

        buttonImage.fillAmount = 1f;
        buttonImage.raycastTarget = true;            
    }
}
