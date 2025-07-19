using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    private float currentTime;
    private float bulletTime = 0.5f;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > bulletTime)   // 타이머가 생성주기를 넘으면
        {
            // 생성 대상 지정, 위치 지정
            GameObject enemy = Instantiate(bulletFactory);
            enemy.transform.position = transform.position;

            // 타이머 초기화
            currentTime = 0f;
        }
    }
}
