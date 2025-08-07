using System;
using UnityEngine;
public class Cannonball : MonoBehaviour
{
    
    public float speed = 10f;
    
    Vector3 dir = Vector3.up;

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        //위쪽으로 나간다
    }       

    //충돌한 게 몬스터이거나 보스면 총알 사라지기
    private void OnCollisionEnter2D(Collision2D other) // 아마 여기서 참조해서 score ++ exp ++... 
    {
        if (other.transform.CompareTag("Monster") || other.transform.CompareTag("Boss")) Destroy(gameObject);
    }
}
