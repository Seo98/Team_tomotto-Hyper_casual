using UnityEngine;

public class s_CameraFollow : MonoBehaviour
{
    public Transform s_playerTarget;
    private Vector3 s_offset;

    void Start()
    {
        if (s_playerTarget != null)
        {
            s_offset = transform.position - s_playerTarget.position;
        }
    }

    void LateUpdate()
    {
        if (s_playerTarget != null)
        { 
            Vector3 newPosition = s_playerTarget.position + s_offset;
            transform.position = newPosition;
        }
    }
}