using UnityEngine;

public class h_EnemyFire : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    private float currentTime;
    public float firetCoolime = 2f;

    private void Start()
    {
        new WaitForSeconds(1f);

        GameObject enemy = Instantiate(bulletFactory);
        enemy.transform.position = transform.position;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > firetCoolime)
        {
            GameObject enemy = Instantiate(bulletFactory);
            enemy.transform.position = transform.position;

            currentTime = 0f;
        }
    }
}
