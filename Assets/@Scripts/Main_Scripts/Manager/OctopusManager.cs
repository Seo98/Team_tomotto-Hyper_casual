using System.Collections;
using UnityEngine;

public class OctopusManager : MonoBehaviour
{
     
    private float currentTime;
    public float createTime = 10;

    public GameObject enemyFactory;

    bool isRunning; 

    void OnEnable()
    {
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;
            enemy.transform.parent = this.transform;

            currentTime = 0f;

            if(!isRunning)
            {
                StartCoroutine(DestroyOctopus());
            }

        }
    }

    IEnumerator DestroyOctopus()
    {
        isRunning = true;

        yield return new WaitForSeconds(20f);

        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        isRunning = false;
    }
}

