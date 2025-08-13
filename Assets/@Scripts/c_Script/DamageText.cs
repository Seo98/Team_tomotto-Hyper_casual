using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageText : MonoBehaviour
{

    public Cannonball fireball;
    //데미지 받으면 텍스트
    public GameObject hudDamageText;
    public Transform hudPos;

    private float moveSpeed = 2f;
    private float alphaSpeed = 2f;
    private float destroyTime = 2f;

    int damage;
    TextMeshProUGUI text;
    Color alpha;
    

    public void Setup(int damage)
    {        
        text = GetComponent<TextMeshProUGUI>();            
        //alpha = text.color;
        text.text = damage.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {      
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        //text.color = alpha;
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }


}

