using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, v, 0);

        transform.position += dir * speed * Time.deltaTime;
    }
}