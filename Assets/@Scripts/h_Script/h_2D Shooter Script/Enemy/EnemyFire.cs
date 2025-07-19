using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    private float currentTime;
    public float bulletTime = 2f;

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > bulletTime)
        {
            GameObject enemy = Instantiate(bulletFactory);
            enemy.transform.position = transform.position;

            currentTime = 0f;
        }
    }
}
