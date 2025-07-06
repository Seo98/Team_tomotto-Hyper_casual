using UnityEngine;
using UnityEngine.UI;

public class c_Obstacle : MonoBehaviour
{

    public enum ObstacleType { SEAWEED, ROCK, WHIRLPOOL, BROKENSHIP }
    public ObstacleType c_obType;


    //PlayerMovement s_playMove;
    bool c_isDamage;
    float c_damage;

    private void Start()
    {
        // s_playMove = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        // S. 오브젝트 태그에 따라 자동지정 
        // 사전에 해당 오브젝트 프리팹화 및 태그지정필요
        /*
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
        }*/
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
                    slowObstacle(true, 30f);
                    break;
                case ObstacleType.WHIRLPOOL:
                    slowObstacle(false, 0);
                    break;
                case ObstacleType.BROKENSHIP:
                    slowObstacle(true, 50f);
                    break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // s_playMove.h_boatSpeed = 3f;
    }

    void slowObstacle(bool c_isDamage, float damage)
    {
        if (c_isDamage) //데미지 입고 느려짐
        {
            // s_playMove.h_boatSpeed *= 0.2f;
            // s_playMove.nowhp -= damage;  //(nowhp = nowhp- damage)
            // s_playMove.hp = s_playMove.nowhp;
            Debug.Log($"데미지를 입었습니다. 현재 hp : ");

        }
        else //데미지 안 입고 느려짐
        {
            // s_playMove.h_boatSpeed *= 0.2f;
            Debug.Log("스턴 걸렸습니다.");
        }
    }
}
