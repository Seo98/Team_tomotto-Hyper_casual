using UnityEngine;

public class h_SpeedUp : MonoBehaviour
{
    private Rigidbody2D h_targetRb;                 // h_밀어낼 대상의 Rigidbody2D
    [SerializeField] private float pushPower = -5f; // h_밀어내는 힘, 에디터에서 조정 가능

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)                     // h_다른 오브젝트가 트리거에 닿으면
    {
        if (other.CompareTag("Boat"))                           // h_만약 그것의 태그가 Boat면
        {
            h_targetRb = other.GetComponent<Rigidbody2D>();     // h_targetRb에 해당 오브젝트의 Rigidbody2D를 대입
            PushBoat();                                         // h_PushBoat 함수 호출
        }
    }

    // private void PushBoat()
    // {
    //     h_targetRb.AddForceY(pushPower, ForceMode2D.Impulse); // h_닿은 오브젝트의 Rigidbody2D에 밀어내는 힘 만큼의 순간적인 힘을 y방향으로 가함
    // }
    //      h_부적절하게 작동해서 뺐지만 일단 참조로 냅둘게요

    private void PushBoat() // h_기존 속도에 일정 속도를 더해줌
    {        
        Vector2 h_currentVelocity = h_targetRb.linearVelocity;
        h_currentVelocity.y += pushPower;
        h_targetRb.linearVelocity = h_currentVelocity;
    }
}
