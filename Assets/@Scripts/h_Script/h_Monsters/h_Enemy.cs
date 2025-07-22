using UnityEngine;
using System;

public class h_Enemy : MonoBehaviour
{
    private s_ScoreManager s_scoremanage;

    private Vector3 dir;

    // 왜 h_ 안붙이나요..................................................................................ㅜㅜㅜ
    [Header("몬스터 능력치")]
    public float h_enSpeed = 2f;
    public float h_enHp = 3f;
    private bool follow = false;

    [Header("몬스터 종류")]
    public GameObject jelly_normal;
    public GameObject shark_follower;
    public GameObject octo_ink;


    [Header("공격 위치")]
    public GameObject enemyFire;

    [Header("추적 대상 이름")]
    public String playerName = "Player"; // 태그사용으로 주석 해제시 사용필요없음 >> 75.B_FIX

    // A_Temp_Fix : Dev_Seo : 병합을 위한 변수 생성 
    public Vector3 h_nowPos;
    public bool h_isDead = false;
    private c_MonsterDropItem dropIt;
    private s_PlayerController s_player; // 플레이어 공격력 참조
    //>>>

    // 사망 효과 인스펙터
    // public GameObject deathIpactFactory;

    void Start()
    {
        // A_Temp_Fix : Dev_Seo // 병합을 위한 할당
        dropIt = GameObject.Find("DropManager").GetComponent<c_MonsterDropItem>();
        s_player = GameObject.FindWithTag("Player").GetComponent<s_PlayerController>(); // 플레이어 공격력 참조
        //>>>>

        shark_follower.SetActive(false);
        octo_ink.SetActive(false);
        jelly_normal.SetActive(false);

        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 2)
        {
            // 상어
            shark_follower.SetActive(true); // 각 스킨은 꺼져 있다가 선택된 타입의 스킨 켜지는 방식
            follow = true;  // Update의 플레이어를 따라다니는 기능 실행을 위한 신호
            h_enHp -= 2f;
            h_enSpeed += 1f;    // 상어는 체력은 낮게, 속도는 빠르게
        }
        else if (ranValue < 5)
        {
            // 문어
            octo_ink.SetActive(true);
            GameObject target = GameObject.FindWithTag("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
            enemyFire.SetActive(true);  // 공격 기능 수행하는 오브젝트 켜기
        }
        else
        {
            // 해파리
            jelly_normal.SetActive(true);
            dir = Vector3.down;
        }        
    }

    void Update()
    {
        transform.position += dir * h_enSpeed * Time.deltaTime;

        if (follow)
            Follow();

        if (h_enHp <= 0)
            Dead();
    }

    void Follow()   // 상어 플레이어 추적 기능
    {
        // B_FIX : Dev_Seo
        //GameObject target = GameObject.Find(playerName);    // 인스펙터상 플레이어 이름 검색
        // >>>>
        // A_Temp_Fix : Dev_Seo
        GameObject target = GameObject.FindWithTag("Player"); 

        dir = target.transform.position - transform.position;
        dir.Normalize();        
    }

    void Dead()
    {
        // 사망 효과
        // GameObject ipact = Instantiate(deathIpactFactory);
        // ipact.transform.position = transform.position;

        h_nowPos = this.transform.position;
        dropIt.DropItem(h_nowPos);
        h_isDead = true;
        

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("fireball"))
        {
            // A_Temp_Fix : Dev_Seo
            if (h_enHp <= 0)
            {
                Dead();
                return;
            }
            // B_FIX : Dev_Seo
            // h_enHp--;
            // >>>>
            // A_Temp_Fix : Dev_Seo
            h_enHp -= s_player.c_Damage;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Dead();
        }
    }


}
