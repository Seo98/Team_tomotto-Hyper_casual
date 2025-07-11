using UnityEngine;
using UnityEngine.UI;

public class s_Intro : MonoBehaviour
{
    [Header("UI 오브젝트")]
    public Button s_Button;
    public GameObject s_IntroObj;
    public GameObject s_StartGame;
    public GameObject s_StartGame2;

    private void Start()
    {
        s_Button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        s_IntroObj.SetActive(false);
        s_StartGame.SetActive(true);
        s_StartGame2.SetActive(true);
    }
}
