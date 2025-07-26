using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("매니저 접근")]
    public GameObject feverManager;
    public GameObject scoreManager;

    [Header("UI 오브젝트(버튼)")]
    public Button button;
    public Button restratButton;

    [Header("UI 오브젝트(부모)")]
    public GameObject introObj;
    public GameObject startGameUI;
    public GameObject startGame;
    public GameObject gameOverUI;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;


    public PlayerController playerController;


    private void Awake()
    {
    }

    private void Start()
    {
        button.onClick.AddListener(StartGame);
        restratButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        introObj.SetActive(false);

        startGameUI.SetActive(true);
        startGame.SetActive(true);
        scoreManager.SetActive(true);
        feverManager.SetActive(true);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);


        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        HpUISetting();

        if (playerController.hp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        introObj.SetActive(false);

        startGameUI.SetActive(false);
        startGame.SetActive(false);
        scoreManager.SetActive(false);
        feverManager.SetActive(false);

        gameOverUI.SetActive(true);

        ClearAllMonsters();
        ClearAllItems();
        ClearAllEnemyBullets();
    }

private void ClearAllMonsters()
    {
        Monster[] monsters = FindObjectsByType<Monster>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        foreach (Monster monster in monsters)
        {
            Destroy(monster.gameObject);
        }
    }

    private void ClearAllItems()
    {
        BonusItem[] item = FindObjectsByType<BonusItem>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        foreach (BonusItem items in item)
        {
            Destroy(items.gameObject);
        }
    }

    private void ClearAllEnemyBullets()
    {
        Inkball[] inkBalls = FindObjectsByType<Inkball>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Inkball inkBall in inkBalls)
        {
            Destroy(inkBall.gameObject);
        }
    }

    private void HpUISetting()
    {
        if (playerController.hp == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        if (playerController.hp == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        if (playerController.hp == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
    }


}