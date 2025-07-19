using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 15;

    public GameObject explosionFactory;
    public GameObject ipactFactory;

    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // 점수 증가
        GameObject smObject = GameObject.Find("ScoreManager");
        ScoreManager sm = smObject.GetComponent<ScoreManager>();

        sm.SetScore(sm.GetScore() + 1);

        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        GameObject ipact = Instantiate(ipactFactory);
        ipact.transform.position = transform.position;

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
