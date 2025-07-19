using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [Header("Rotation Details")]
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float controlPitchFactor = 30f;
    [SerializeField] private float controlYawFactor = 15f;

    [SerializeField] public float minX = -5f, maxX = 5f;
    [SerializeField] public float minY = -3f, maxY = 3f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        transform.position += dir * speed * Time.deltaTime;
        ApplyRotation(h, v);

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void ApplyRotation(float h, float v)
    {
        float pitch = controlPitchFactor * v;
        float yaw = -controlYawFactor * h;
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, yaw);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
