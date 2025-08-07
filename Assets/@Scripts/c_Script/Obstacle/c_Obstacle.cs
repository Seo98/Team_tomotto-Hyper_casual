using UnityEngine;
using UnityEngine.UI;

public class c_Obstacle : MonoBehaviour
{ 
    public enum ObstacleType { SEAWEED, ROCK, WHIRLPOOL, BROKENSHIP }
    public ObstacleType c_obType;

    PlayerController player;
    FeverTimeManager feverTime;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        feverTime = FindFirstObjectByType<FeverTimeManager>();
        
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

        if (feverTime.isFever)
        {
            this.gameObject.SetActive(false);
            return;
        }
        
        if (other.CompareTag("fireball"))
        {
            if (this.gameObject.tag == "Rock" || this.gameObject.tag == "BrokenShip")          
               this.gameObject.SetActive(false);            
        }          
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.collider.CompareTag("Player"))
        {
            feverTime.nowGauge = feverTime.feverImage.fillAmount - 0.1f; //충돌 시 게이지 깎임
            feverTime.feverImage.fillAmount = feverTime.nowGauge; 

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
        player.moveSpeed = 5f;
    }
    void slowObstacle(bool c_isDamage, float damage)
    {
        if (c_isDamage) //데미지 입고 느려짐
        {
            player.moveSpeed *= 0.2f;
            player.hp -= damage;  //(hp = hp- damage)   
            Debug.Log($"데미지를 입었습니다. 현재 hp : {player.hp}");          
        }
        else //데미지 안 입고 느려짐
        {
            player.moveSpeed *= 0.2f;
            Debug.Log("스턴 걸렸습니다.");
        }
    }
}
