using UnityEngine;

public class c_BonusItem : MonoBehaviour
{
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType c_bonusItem;

    public GameObject c_ShiledHeart;
    public GameObject c_Shiledimage;

    SpriteRenderer c_sr;
    s_PlayerController player;
    c_Cannonball c_fireball;

    bool isShield;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<s_PlayerController>();
        c_sr = this.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            c_sr.enabled = false;
            if (this.c_bonusItem == BonusType.SHEILD)
            {
                c_Shield();               
            }
            if(this.c_bonusItem == BonusType.ATTACKUP)
            {
                c_AttackUP();
            }
        }
    }

    void c_Shield() //파란 하트 추가로 생기고 플레이어 주변에 파란색 막 생기기... 장애물 부딪히면 파란색 하트와 막 같이 제거
    {
        
        while(isShield)
        {
            c_ShiledHeart.SetActive(true);
            c_Shiledimage.SetActive(true);
            break;
        }        
        //공격 받으면 꺼짐
    }

    void c_AttackUP()//단순히 대포알 생성 주기 빨라지고 
    {
        c_fireball.c_speed = 13f; //공격 스피드 업
        player.c_spawnTime = 1f; //대포 스폰 시간 줄임
        c_fireball.c_Damage = 2; //데미지 업
    }
}
