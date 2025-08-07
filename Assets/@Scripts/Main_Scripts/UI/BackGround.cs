using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Dev_s : 승표라인
    public Material bgMaterial; 

    public float scrollSpeed = 0.2f;
    public PlayerController playerController;

    private void OnEnable()
    {
        bgMaterial.mainTextureOffset = Vector2.zero;
    }

    void Update()
    {
        float speed = scrollSpeed + playerController.moveSpeed;

        bgMaterial.mainTextureOffset += Vector2.up * speed * Time.deltaTime;
    }
}

