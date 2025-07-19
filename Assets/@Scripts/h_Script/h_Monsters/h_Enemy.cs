using UnityEngine;
using System;

public class h_Enemy : MonoBehaviour
{
    private Vector3 dir;
    private float speed = 8f;
    private bool follow = false;

    public GameObject jelly_normal;
    public GameObject shark_follower;
    public GameObject octo_ink;
    public GameObject enemyFire;

    // 사망 효과 인스펙터
    // public GameObject explosionFactory;
    // public GameObject ipactFactory;

    void Start()
    {
        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 2)
        {
            // 상어
            follow = true;  // Update의 플레이어를 따라다니는 기능 실행을 위한 신호

            shark_follower.SetActive(true); // 각 스킨은 꺼져 있다가 선택된 타입의 스킨 켜지는 방식
        }
        else if (ranValue < 5)
        {
            // 문어
            GameObject target = GameObject.FindWithTag("Boat");   // Boat라는 태그 검색
            dir = target.transform.position - transform.position;
            dir.Normalize();

            octo_ink.SetActive(true);
            enemyFire.SetActive(true);  // 공격 기능 수행하는 오브젝트 켜기
        }
        else
        {
            // 해파리
            dir = Vector3.down;
            speed = 5f;

            jelly_normal.SetActive(true);
        }        
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        if (follow)
        {
            GameObject target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();

            speed = 10f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // 점수 증가
        GameObject smObject = GameObject.Find("ScoreManager");
        ScoreManager sm = smObject.GetComponent<ScoreManager>();

        sm.SetScore(sm.GetScore() + 1);

        // 사망 효과
        // GameObject explosion = Instantiate(explosionFactory);
        // explosion.transform.position = transform.position;
        // 
        // GameObject ipact = Instantiate(ipactFactory);
        // ipact.transform.position = transform.position;

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
