using System.Collections;
using UnityEngine;

public class c_ObstacleSpawner : MonoBehaviour
{
    public c_ObstaclePool obstaclePool;
    public s_PlayerController player;
    public float spawnInterval = 3f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            GameObject obc = obstaclePool.DequeueObc();

            // ��ġ ���� ����
            float x = Random.Range(-2.8f, 2.8f);
            float y = Random.Range(-9.5f, -5.5f);
            Vector2 spawnPos = (Vector2)player.transform.position + new Vector2(x, y);
            obc.transform.position = spawnPos;

            // ������Ʈ ���� ����
            var returnScript = obc.GetComponent<c_ObstacleReturnPool>();
            returnScript.player = player;
            returnScript.pool = obstaclePool;

            obc.SetActive(true); // Ȱ��ȭ �� OnEnable ȣ���

            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
