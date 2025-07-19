using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 10;
    public GameObject explosion;

    void Update()
    {
        Vector3 dir = Vector3.up;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        explosion.SetActive(true);
    }

}
