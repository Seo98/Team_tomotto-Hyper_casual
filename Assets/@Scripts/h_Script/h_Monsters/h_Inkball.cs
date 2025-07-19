using UnityEngine;

public class h_Inkball : MonoBehaviour
{
    public float speed = 15;

    // 충돌 효과 인스펙터
    // public GameObject explosionFactory;
    // public GameObject ipactFactory;

    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // 점수 증가
        // GameObject smObject = GameObject.Find("ScoreManager");
        // ScoreManager sm = smObject.GetComponent<ScoreManager>();

        // sm.SetScore(sm.GetScore() + 1);

        // 충돌 효과
        // GameObject explosion = Instantiate(explosionFactory);
        // explosion.transform.position = transform.position;
        // 
        // GameObject ipact = Instantiate(ipactFactory);
        // ipact.transform.position = transform.position;

        
        Destroy(gameObject);
    }
}
