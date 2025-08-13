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


    void Start()
    {
        // 컴포넌트 초기화를 Start에서 미리 처리
        text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            alpha = text.color;
        }
    }

    public void Setup(int damage)
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        alpha = text.color;

        text.text = damage.ToString();

        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {
        if (text == null) return;

        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }


}

