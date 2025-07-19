using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private Vector3 dir;
    private float speed = 5f;
    private bool follow = false;

    public GameObject explosionFactory;
    public GameObject ipactFactory;

    public GameObject firePlane;
    public GameObject followPlane;
    public GameObject fastPlane;
    public GameObject enemyFire;

    void Start()
    {
        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 4)
        {
            GameObject target = GameObject.Find("Player");          // Find�� ȿ�������� ������ Start�� �ѹ��� �ϴ� ����
            dir = target.transform.position - transform.position;   // ��ǥ ��ġ - ���� ��ġ = ���翡�� ��ǥ�� ���� ����
            dir.Normalize();

            firePlane.SetActive(true);
            enemyFire.SetActive(true);
        }
        else if (ranValue < 7)
        {
            follow = true;

            followPlane.SetActive(true);
        }
        else
        {
            dir = Vector3.down;
            speed = 8f;

            fastPlane.SetActive(true);
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

        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;

        GameObject ipact = Instantiate(ipactFactory);
        ipact.transform.position = transform.position;

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
