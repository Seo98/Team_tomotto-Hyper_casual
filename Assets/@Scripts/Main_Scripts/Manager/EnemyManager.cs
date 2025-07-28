using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // dev_s: 시웅님라인, 클래스 이름 EnemyManager 보다 Spawner가 더 괜찮은거같아요 참고만 해주세요!
    private float currentTime;

    public float createTime = 8;

    public GameObject enemyFactory;

    void OnEnable()
    {
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {    
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;
            enemy.transform.parent = this.transform;

            currentTime = 0f;
            
        }
    } 
}