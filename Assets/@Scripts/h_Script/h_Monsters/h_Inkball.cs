using UnityEngine;

public class h_Inkball : MonoBehaviour
{
    public float speed = 15;

    // �浹 ȿ�� �ν�����
    // public GameObject explosionFactory;
    // public GameObject ipactFactory;

    void Update()
    {
        Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        // ���� ����
        // GameObject smObject = GameObject.Find("ScoreManager");
        // ScoreManager sm = smObject.GetComponent<ScoreManager>();

        // sm.SetScore(sm.GetScore() + 1);

        // �浹 ȿ��
        // GameObject explosion = Instantiate(explosionFactory);
        // explosion.transform.position = transform.position;
        // 
        // GameObject ipact = Instantiate(ipactFactory);
        // ipact.transform.position = transform.position;

        
        Destroy(gameObject);
    }
}
