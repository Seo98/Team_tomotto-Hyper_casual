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

            // ���� ������ ����
            PlayerPrefs.SetInt("Score", score);

            int loadScore = PlayerPrefs.GetInt("Score");

            PlayerPrefs.SetInt("Score", score);         // 
            PlayerPrefs.SetFloat("Volume", 0.5f);       // 
            PlayerPrefs.SetString("UserName", "Jhon");  // �ش� Ű �� ����

            PlayerPrefs.DeleteKey("Score");             // 
            PlayerPrefs.DeleteKey("Volume");            // 
            PlayerPrefs.DeleteKey("UserName");          // �ش� Ű �� ����

            PlayerPrefs.DeleteAll();                    // ��� Ű �� ����

            PlayerPrefs.Save();                         // ����� �� �ڵ� ����
        }
    }
}
