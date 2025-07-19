using UnityEngine;

public class LocalData : MonoBehaviour
{
    private int score;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            score++;

            // 로컬 데이터 저장
            PlayerPrefs.SetInt("Score", score);

            int loadScore = PlayerPrefs.GetInt("Score");

            PlayerPrefs.SetInt("Score", score);         // 
            PlayerPrefs.SetFloat("Volume", 0.5f);       // 
            PlayerPrefs.SetString("UserName", "Jhon");  // 해당 키 값 생성

            PlayerPrefs.DeleteKey("Score");             // 
            PlayerPrefs.DeleteKey("Volume");            // 
            PlayerPrefs.DeleteKey("UserName");          // 해당 키 값 삭제

            PlayerPrefs.DeleteAll();                    // 모든 키 값 삭제

            PlayerPrefs.Save();                         // 종료될 때 자동 저장
        }
    }
}
