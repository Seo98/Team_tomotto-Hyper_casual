using UnityEngine;

public class c_BonusItem : MonoBehaviour
{
    public enum BonusType { SHEILD, ATTACKUP };
    public BonusType c_bonusItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(CompareTag("Player"))
        {
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

    }

    void c_AttackUP()//단순히 대포알 접근해서 스피드 올리기
    {

    }
}
