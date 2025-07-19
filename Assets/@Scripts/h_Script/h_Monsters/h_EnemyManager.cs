using UnityEngine;

public class h_EnemyManager : MonoBehaviour
{
    private float currentTime;  // 타이머

    private float minTime = 1;
    private float maxTime = 5;

    public float createTime = 1;    // 생성주기

    public GameObject enemyFactory;

    void Start()
    {
        createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)   // 타이머가 생성주기를 넘으면
        {
            // 생성 대상 지정, 위치 지정
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;

            // 타이머 초기화
            currentTime = 0f;
        }
    }
}
