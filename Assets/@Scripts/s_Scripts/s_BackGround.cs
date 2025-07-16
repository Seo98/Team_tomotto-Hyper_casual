using UnityEngine;

public class s_BackGround : MonoBehaviour
{
    public Material s_bgMaterial;

    public float s_scrollSpeed = 0.2f;

    void Update()
    {
        Vector2 direction = Vector2.up;

        s_bgMaterial.mainTextureOffset += direction * s_scrollSpeed * Time.deltaTime;
    }
}

