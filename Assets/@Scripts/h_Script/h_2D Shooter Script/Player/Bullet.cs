using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;

    void Update()
    {
        Vector3 dir = Vector3.up;

        transform.position += dir * speed * Time.deltaTime;
    }
}