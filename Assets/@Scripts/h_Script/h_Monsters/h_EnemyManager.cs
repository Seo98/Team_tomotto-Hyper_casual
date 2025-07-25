using UnityEngine;

public class h_EnemyManager : MonoBehaviour
{
    private float currentTime;  // Ÿ�̸�

    //private float minTime = 1;
    //private float maxTime = 5;

    public float createTime = 8;    // �����ֱ�

    public GameObject enemyFactory;

    void OnEnable()
    {
        //createTime = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)   // Ÿ�̸Ӱ� �����ֱ⸦ ������
        {
            // ���� ��� ����, ��ġ ����
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;
            enemy.transform.parent = this.transform;

            // Ÿ�̸� �ʱ�ȭ
            currentTime = 0f;
        }
    }
}
