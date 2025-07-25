using UnityEngine;

public class s_BackGround : MonoBehaviour
{
    public Material s_bgMaterial;

    public float s_scrollSpeed = 0.2f;
    public s_PlayerController s_PlayerController;

    private void OnEnable()
    {
        s_bgMaterial.mainTextureOffset = Vector2.zero;
    }

    void Update()
    {
        float speed = s_scrollSpeed + s_PlayerController.c_moveSpeed;

        s_bgMaterial.mainTextureOffset += Vector2.up * speed * Time.deltaTime;
    }
}

