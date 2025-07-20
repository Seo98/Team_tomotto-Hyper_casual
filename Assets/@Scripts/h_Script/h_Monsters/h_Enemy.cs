using UnityEngine;
using System;

public class h_Enemy : MonoBehaviour
{
    private s_ScoreManager s_scoremanage;

    private Vector3 dir;

    [Header("���� �ɷ�ġ")]
    public float h_enSpeed = 2f;
    public float h_enHp = 3f;
    private bool follow = false;

    [Header("���� ����")]
    public GameObject jelly_normal;
    public GameObject shark_follower;
    public GameObject octo_ink;

    [Header("���� ��ġ")]
    public GameObject enemyFire;

    [Header("���� ��� �̸�")]
    public String playerName = "��Player";

    // ��� ȿ�� �ν�����
    // public GameObject deathIpactFactory;

    void Start()
    {
        shark_follower.SetActive(false);
        octo_ink.SetActive(false);
        jelly_normal.SetActive(false);

        int ranValue = UnityEngine.Random.Range(0, 10);

        if (ranValue < 2)
        {
            // ���
            shark_follower.SetActive(true); // �� ��Ų�� ���� �ִٰ� ���õ� Ÿ���� ��Ų ������ ���
            follow = true;  // Update�� �÷��̾ ����ٴϴ� ��� ������ ���� ��ȣ
            h_enHp -= 2f;
            h_enSpeed += 1f;    // ���� ü���� ����, �ӵ��� ������
        }
        else if (ranValue < 5)
        {
            // ����
            octo_ink.SetActive(true);
            GameObject target = GameObject.FindWithTag("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
            enemyFire.SetActive(true);  // ���� ��� �����ϴ� ������Ʈ �ѱ�
        }
        else
        {
            // ���ĸ�
            jelly_normal.SetActive(true);
            dir = Vector3.down;
        }        
    }

    void Update()
    {
        transform.position += dir * h_enSpeed * Time.deltaTime;

        if (follow)
            Follow();

        if (h_enHp <= 0)
            Dead();
    }

    void Follow()   // ��� �÷��̾� ���� ���
    {
        GameObject target = GameObject.Find(playerName);    // �ν����ͻ� �÷��̾� �̸� �˻�
        dir = target.transform.position - transform.position;
        dir.Normalize();        
    }

    void Dead()
    {
        // ��� ȿ��
        // GameObject ipact = Instantiate(deathIpactFactory);
        // ipact.transform.position = transform.position;

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            h_enHp--;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Dead();
        }
    }
}
