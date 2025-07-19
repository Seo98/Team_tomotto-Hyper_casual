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

        if (currentTime > bulletTime)   // Ÿ�̸Ӱ� �����ֱ⸦ ������
        {
            // ���� ��� ����, ��ġ ����
            GameObject enemy = Instantiate(bulletFactory);
            enemy.transform.position = transform.position;

            // Ÿ�̸� �ʱ�ȭ
            currentTime = 0f;
        }
    }
}
