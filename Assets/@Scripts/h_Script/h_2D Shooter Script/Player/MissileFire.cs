using UnityEngine;
using UnityEngine.UI;

public class MissileFire : MonoBehaviour
{
    public GameObject missileFactory;
    public GameObject firePosition;

    public Image missileCooltime;

    private float currentTime;
    private float missileTime = 5;

    void Update()
    {
        currentTime += Time.deltaTime;

        missileCooltime.fillAmount = currentTime / missileTime;

        if (currentTime > missileTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject missile = Instantiate(missileFactory);
                missile.transform.position = firePosition.transform.position; // 위치 초기화

                currentTime = 0f;
            }
        }
    }
}