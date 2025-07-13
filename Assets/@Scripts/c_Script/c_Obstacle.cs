using UnityEngine;
using UnityEngine.UI;

public class c_Obstacle : MonoBehaviour
{ 
    public enum ObstacleType { SEAWEED, ROCK, WHIRLPOOL, BROKENSHIP }
    public ObstacleType c_obType;

    s_PlayerInfo playerInfo;
    c_FeverTimeManage feverTIme;
    

    private void Start()
    {
        playerInfo = GameObject.FindWithTag("Player").GetComponent<s_PlayerInfo>();

        // S. 오브젝트 태그에 따라 자동지정 
        // 사전에 해당 오브젝트 프리팹화 및 태그지정필요
        
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
        //while (feverTIme.c_isFever)
        //{
            
        //    this.gameObject.SetActive(false);

        //    break;
        //}

        if (other.CompareTag("fireball"))
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
        //while (feverTIme.c_isFever)
        //{
        //    collision.collider.isTrigger = true;
        //    this.gameObject.SetActive(false);

        //    break;
        //}

        if (collision.collider.CompareTag("Player"))
        {
            if (feverTIme.c_isFever)
                return;

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
        if (c_isDamage) //데미지 입고 느려짐
        {
            playerInfo.s_moveSpeed *= 0.2f;
            playerInfo.s_hp -= damage;  //(hp = hp- damage)   
            Debug.Log($"데미지를 입었습니다. 현재 hp : {playerInfo.s_hp}");

        }
        else //데미지 안 입고 느려짐
        {
            playerInfo.s_moveSpeed *= 0.2f;
            Debug.Log("스턴 걸렸습니다.");
        }
    }
}
