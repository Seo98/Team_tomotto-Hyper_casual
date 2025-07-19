using UnityEngine;

public class FollowMissile : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private Vector3 defaultDirection;

    void Start()
    {
        target = FindClosestEnemy();

        if (target == null)
        {
            defaultDirection = transform.forward;
        }
    }

    void Update()
    {
        Vector3 dir;

        if (target != null)
        {
            dir = (target.position - transform.position).normalized;
            transform.LookAt(target);
        }
        else
        {
            dir = defaultDirection;
        }

        transform.position += dir * speed * Time.deltaTime;
    }

    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = enemy.transform;
            }
        }

        return closest;
    }
}
