using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    private float currentTime;
    public float bulletTime = 2f;

    private void Start()
    {
        new WaitForSeconds(0.5f);

        GameObject enemy = Instantiate(bulletFactory);
        enemy.transform.position = transform.position;
    }

    private void Update()
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
