using System;
using System.Collections;
using UnityEngine;
public class Cannonball : MonoBehaviour
{
    public float speed = 10f;
    public int fireDamage = 1;
    public GameObject damageTextPrefab;
    public Transform damageTextPos;

    Vector3 dir = Vector3.up;
    Monster monster;
    PlayerController player;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
        
       
    }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        //위쪽으로 나간다
        player.damageConnect(this.fireDamage);


    }

    //충돌한 게 몬스터이거나 보스면 총알 사라지기
    private void OnCollisionEnter2D(Collision2D other) // 아마 여기서 참조해서 score ++ exp ++... 
    {
        Monster monster = other.transform.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(fireDamage);
            GameObject dmgObj = Instantiate(damageTextPrefab, damageTextPos.position, Quaternion.identity);
            dmgObj.GetComponent<DamageText>().Setup(fireDamage);
        }

        if (other.transform.CompareTag("Monster") || other.transform.CompareTag("Boss")) Destroy(this.gameObject);

    }
}
