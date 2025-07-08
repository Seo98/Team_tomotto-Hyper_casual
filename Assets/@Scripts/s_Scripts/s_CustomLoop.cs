using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class s_CustomLoop : MonoBehaviour
{
    /*
    [SerializeField] private Tilemap s_targetTilemap;
    public float s_scrollSpeed = 0.5f;
    */

    public float s_moveSpeed = 3f;
    private float s_returnPosY = 20f;
    private float s_returnPosX = 20f;
    private float s_returnMPosX = -20f;

    public float s_movedir = 0;

    void Update()
    {
        /*
        // 
        if (s_targetTilemap == null)
        {
            Debug.LogWarning("�Ҵ��ض�");
            enabled = false; 
            return;
        }
        s_targetTilemap.transform.position += Vector3.down * s_scrollSpeed * Time.deltaTime;*/

        transform.position += Vector3.up * s_moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.right * s_moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.left * s_moveSpeed * Time.deltaTime;
        }

        if (transform.position.y >= s_returnPosY)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        if (transform.position.x >= s_returnPosX)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        if (transform.position.x <= s_returnMPosX)
        {
            transform.position = new Vector3(0, 0, 0);
        }

    }
}