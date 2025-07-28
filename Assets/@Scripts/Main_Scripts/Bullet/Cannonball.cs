using System;
using UnityEngine;
public class Cannonball : MonoBehaviour
{
    // dev_s: 은주님 라인, 제가 손본거 따로 없을겁니다 아마도ㅓ
    public float speed = 10f;
    
    Vector3 dir = Vector3.up;

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }       

    private void OnCollisionEnter2D(Collision2D other) // 아마 여기서 참조해서 score ++ exp ++... 
    {
        if (other.transform.CompareTag("Monster")) Destroy(gameObject);
    }
}
