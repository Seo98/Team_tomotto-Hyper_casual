using System;
using UnityEngine;
public class c_Cannonball : MonoBehaviour
{
    public float speed = 5;
    
    private void Update()
    {
        Vector3 dir = Vector3.up;

        transform.position += dir * speed * Time.deltaTime;
    }    
}
