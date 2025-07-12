using System.Collections;
using UnityEngine;

public class c_ObstacleReturnPool : MonoBehaviour
{
    public s_PlayerController player;
    public c_ObstaclePool pool;

    private void OnEnable()
    {
        Invoke("ReturnPool", 15f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void ReturnPool() 
    {
        if(pool != null)
          pool.EnqueueObc(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke();
            Invoke("ReturnToPool", 10f); // 충돌 시 10초 뒤 반환
        }
    }
}
