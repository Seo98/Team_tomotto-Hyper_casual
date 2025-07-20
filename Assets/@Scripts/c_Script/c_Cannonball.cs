using System;
using UnityEngine;
public class c_Cannonball : MonoBehaviour
{
    //float power = 2f;
    public int c_Damage = 1;
    public float c_speed = 10f;
    
    Vector3 c_dir = Vector3.up;

    private void Update()
    {
        //Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();
        //rb.AddForceY(power, ForceMode2D.Impulse);

        transform.position += c_dir * c_speed * Time.deltaTime;
    }       

    void TakeDamage(int damage)
    {
        c_Damage = 1;
    }
}
