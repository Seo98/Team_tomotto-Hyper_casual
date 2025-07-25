using UnityEngine;
using System;

public class h_Enemy : MonoBehaviour
{
    private s_ScoreManager s_scoremanage;

    private Vector3 dir;

    // �� h_ �Ⱥ��̳���..................................................................................�̤̤�
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
    public String playerName = "Player"; // �±׻������ �ּ� ������ ����ʿ���� >> 75.B_FIX

    // A_Temp_Fix : Dev_Seo : ������ ���� ���� ���� 
    public Vector3 h_nowPos;
    public bool h_isDead = false;
    private c_MonsterDropItem dropIt;
    private s_PlayerController s_player; // �÷��̾� ���ݷ� ����
    private GameObject spawnerType;
    private float currentTime = 0f;
    private float spawnTime = 2f;
    //>>>

    // ��� ȿ�� �ν�����
    // public GameObject deathIpactFactory;

    void Start()
    {
        // A_Temp_Fix : Dev_Seo // ������ ���� �Ҵ�
        dropIt = GameObject.Find("DropManager").GetComponent<c_MonsterDropItem>();
        s_player = GameObject.FindWithTag("Player").GetComponent<s_PlayerController>(); // �÷��̾� ���ݷ� ����
        spawnerType = this.gameObject;
        //>>>>

        shark_follower.SetActive(false);
        octo_ink.SetActive(false);
        jelly_normal.SetActive(false);
      
    }

    void Spawn()
    {
        if (spawnerType.CompareTag("Shark"))
        {
            // ���
            shark_follower.SetActive(true); // �� ��Ų�� ���� �ִٰ� ���õ� Ÿ���� ��Ų ������ ���
            follow = true;  // Update�� �÷��̾ ����ٴϴ� ��� ������ ���� ��ȣ
            h_enHp -= 2f;
            h_enSpeed += 1f;    // ���� ü���� ����, �ӵ��� ������
        }
        else if (spawnerType.CompareTag("Oct"))
        {
            // ����
            octo_ink.SetActive(true);
            GameObject target = GameObject.FindWithTag("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
            enemyFire.SetActive(true);  // ���� ��� �����ϴ� ������Ʈ �ѱ�
        }
        else if (spawnerType.CompareTag("BaseMob"))
        {
            // ���ĸ�
            jelly_normal.SetActive(true);
            dir = Vector3.down;
        }

        currentTime = 0f;

    }
    // �̰� Enemy ��ũ��Ʈ ���� �ϴ� �ٲ�ߵɰŰ��ƿ� �����غôµ�
    // ���� ������ ������Ʈ....

    void Update()
    {
        transform.position += dir * h_enSpeed * Time.deltaTime;
        currentTime += Time.deltaTime;

        if (follow)
            Follow();

        if (h_enHp <= 0)
            Dead();

        if (spawnTime < currentTime)
            Spawn();
    }

    void Follow()   // ��� �÷��̾� ���� ���
    {
        // B_FIX : Dev_Seo
        //GameObject target = GameObject.Find(playerName);    // �ν����ͻ� �÷��̾� �̸� �˻�
        // >>>>
        // A_Temp_Fix : Dev_Seo
        GameObject target = GameObject.FindWithTag("Player"); 

        dir = target.transform.position - transform.position;
        dir.Normalize();        
    }

    void Dead()
    {
        // ��� ȿ��
        // GameObject ipact = Instantiate(deathIpactFactory);
        // ipact.transform.position = transform.position;

        h_nowPos = this.transform.position;
        dropIt.DropItem(h_nowPos);
        h_isDead = true;
        

        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("fireball"))
        {
            // A_Temp_Fix : Dev_Seo
            if (h_enHp <= 0)
            {
                Dead();
                return;
            }
            // B_FIX : Dev_Seo
            // h_enHp--;
            // >>>>
            // A_Temp_Fix : Dev_Seo
            h_enHp -= s_player.c_Damage;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Dead();
        }
    }


}
