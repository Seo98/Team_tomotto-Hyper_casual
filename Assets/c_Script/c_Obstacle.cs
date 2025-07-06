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

        // S. ������Ʈ �±׿� ���� �ڵ����� 
        // ������ �ش� ������Ʈ ������ȭ �� �±������ʿ�
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
        if (c_isDamage) //������ �԰� ������
        {
            // s_playMove.h_boatSpeed *= 0.2f;
            // s_playMove.nowhp -= damage;  //(nowhp = nowhp- damage)
            // s_playMove.hp = s_playMove.nowhp;
            Debug.Log($"�������� �Ծ����ϴ�. ���� hp : ");

        }
        else //������ �� �԰� ������
        {
            // s_playMove.h_boatSpeed *= 0.2f;
            Debug.Log("���� �ɷȽ��ϴ�.");
        }
    }
}
