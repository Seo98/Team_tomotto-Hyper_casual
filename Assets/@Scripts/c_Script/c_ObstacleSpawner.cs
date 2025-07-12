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

            // 위치 랜덤 설정
            float x = Random.Range(-2.8f, 2.8f);
            float y = Random.Range(-9.5f, -5.5f);
            Vector2 spawnPos = (Vector2)player.transform.position + new Vector2(x, y);
            obc.transform.position = spawnPos;

            // 컴포넌트 참조 주입
            var returnScript = obc.GetComponent<c_ObstacleReturnPool>();
            returnScript.player = player;
            returnScript.pool = obstaclePool;

            obc.SetActive(true); // 활성화 → OnEnable 호출됨

            yield return new WaitForSeconds(spawnInterval);
        }
    }

}
