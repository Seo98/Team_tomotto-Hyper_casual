using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // dev_s: 시웅님라인, 클래스 이름 EnemyManager 보다 Spawner가 더 괜찮은거같아요 참고만 해주세요!
    private float currentTime;
    public float createTime = 8;

    public GameObject enemyFactory;
    public BossSpawner bossSpawner;


    void OnEnable()
    {
        bossSpawner = FindFirstObjectByType<BossSpawner>();
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

        if(bossSpawner.isBossSpawning == true)
        {
            this.gameObject.SetActive(false);
        }
    } 
}