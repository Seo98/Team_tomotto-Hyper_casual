using UnityEngine;
using System;

public class h_Enemy : MonoBehaviour
{
    private Vector3 dir;
    private float speed = 8f;
    private bool follow = false;

    public GameObject jelly_normal;
    public GameObject shark_follower;
    public GameObject octo_ink;
    public GameObject enemyFire;

    // ��� ȿ�� �ν�����
    // public GameObject explosionFactory;
    // public GameObject ipactFactory;

    void Start()
    {
        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 2)
        {
            // ���
            follow = true;  // Update�� �÷��̾ ����ٴϴ� ��� ������ ���� ��ȣ

            shark_follower.SetActive(true); // �� ��Ų�� ���� �ִٰ� ���õ� Ÿ���� ��Ų ������ ���
        }
        else if (ranValue < 5)
        {
            // ����
            GameObject target = GameObject.FindWithTag("Boat");   // Boat��� �±� �˻�
            dir = target.transform.position - transform.position;
            dir.Normalize();

            octo_ink.SetActive(true);
            enemyFire.SetActive(true);  // ���� ��� �����ϴ� ������Ʈ �ѱ�
        }
        else
        {
            // ���ĸ�
            dir = Vector3.down;
            speed = 5f;

            jelly_normal.SetActive(true);
        }        
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        if (follow)
        {
            GameObject target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();

            speed = 10f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // ���� ����
        GameObject smObject = GameObject.Find("ScoreManager");
        ScoreManager sm = smObject.GetComponent<ScoreManager>();

        sm.SetScore(sm.GetScore() + 1);

        // ��� ȿ��
        // GameObject explosion = Instantiate(explosionFactory);
        // explosion.transform.position = transform.position;
        // 
        // GameObject ipact = Instantiate(ipactFactory);
        // ipact.transform.position = transform.position;

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
