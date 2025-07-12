using UnityEngine;
using UnityEngine.UI;

public class c_Obstacle : MonoBehaviour
{ 
    public enum ObstacleType { SEAWEED, ROCK, WHIRLPOOL, BROKENSHIP }
    public ObstacleType c_obType;

    c_ObstaclePool obcPool;
    s_PlayerInfo playerInfo;
    bool c_isDamage;
    float c_damage;

    private void Start()
    {
        playerInfo = GameObject.FindWithTag("Player").GetComponent<s_PlayerInfo>();

        // S. ������Ʈ �±׿� ���� �ڵ����� 
        // ������ �ش� ������Ʈ ������ȭ �� �±������ʿ�
        
        switch (gameObject.tag)
        {
            case "Seaweed":
                c_obType = ObstacleType.SEAWEED;
                break;
            case "Rock":
                c_obType = ObstacleType.ROCK;
                break;
            case "Whirlpool":
                c_obType = ObstacleType.BROKENSHIP;
                break;
            case "Brokenship":
              c_obType = ObstacleType.WHIRLPOOL;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("fireball"))
        {
            if(this.gameObject.tag == "Rock")
            {
               this.gameObject.SetActive(false);
            }
            if(this.gameObject.tag == "BrokenShip")
            {
                this.gameObject.SetActive(false);
            }
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {

            switch (c_obType)
            {
                case ObstacleType.SEAWEED:
                    slowObstacle(false, 0);
                    break;
                case ObstacleType.ROCK:
                    slowObstacle(true, 1f);
                    break;
                case ObstacleType.WHIRLPOOL:
                    slowObstacle(false, 0);
                    break;
                case ObstacleType.BROKENSHIP:
                    slowObstacle(true, 2f);
                    break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerInfo.s_moveSpeed = 5f;
    }

    void slowObstacle(bool c_isDamage, float damage)
    {
        if (c_isDamage) //������ �԰� ������
        {
            playerInfo.s_moveSpeed *= 0.2f;
            playerInfo.s_hp -= damage;  //(hp = hp- damage)   
            Debug.Log($"�������� �Ծ����ϴ�. ���� hp : {playerInfo.s_hp}");

        }
        else //������ �� �԰� ������
        {
            playerInfo.s_moveSpeed *= 0.2f;
            Debug.Log("���� �ɷȽ��ϴ�.");
        }
    }
}
