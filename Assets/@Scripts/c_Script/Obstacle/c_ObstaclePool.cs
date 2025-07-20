using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class c_ObstaclePool : MonoBehaviour
{
    public Queue<GameObject> c_obcQueue = new Queue<GameObject>();
    public GameObject[] c_obcPrefabs;
    public Transform c_parent;

    private void Start()
    {
        CreateObstacle();
    }

    void CreateObstacle()
    {
        for (int i = 0; i < 50; i++)
        {
            for(int j = 0; j <c_obcPrefabs.Length; j++)
            {
                GameObject obstacle = Instantiate(c_obcPrefabs[j], c_parent);
                obstacle.SetActive(false);
                c_obcQueue.Enqueue(obstacle);
            }
        }
    }

    public void EnqueueObc(GameObject obc)
    {     
        obc.SetActive(false);
        c_obcQueue.Enqueue(obc);
    }
    public GameObject DequeueObc()
    {
        if (c_obcQueue.Count < 5)
            CreateObstacle();

        return c_obcQueue.Dequeue();
    }
}
