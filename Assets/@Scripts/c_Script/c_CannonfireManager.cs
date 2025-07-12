using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class c_CannonfireManager : MonoBehaviour
{
    public Transform c_firePos;
    public GameObject c_ballPrefab;
    public Button c_fireButton;

    public float c_shootPower = 20f;

    private void Awake()
    { 
        c_fireButton.onClick.AddListener(CannonFire);
    }

    void CannonFire()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {     
        GameObject c_fireball = Instantiate(c_ballPrefab, c_firePos);
        Rigidbody2D rb = c_fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(Vector2.down * c_shootPower, ForceMode2D.Impulse);            
        }

        yield return new WaitForSeconds(5f);
    }
}
