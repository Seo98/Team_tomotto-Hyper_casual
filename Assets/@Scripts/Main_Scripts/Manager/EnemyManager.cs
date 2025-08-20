using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // dev_s: 시웅님라인, 클래스 이름 EnemyManager 보다 Spawner가 더 괜찮은거같아요 참고만 해주세요!
    public float currentTime;
    public float createTime = 12;
    public GameObject enemyFactory;

    private int randomDistroy;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            randomDistroy = Random.Range(1, 4);
            currentTime = 0f;
            
            // if (randomDistroy <= 1)
            //     return;

            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(enemyFactory);
        enemy.transform.position = transform.position;
        enemy.transform.parent = this.transform;
    }
}