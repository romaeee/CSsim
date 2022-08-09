using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text cartText;

    private bool isPaused;
    public static int currentMoney = 1000;

    public static Dictionary<int, int> piriceData = new Dictionary<int, int>();

    private void Awake()
    {
        piriceData.Add(0, 300);
        piriceData.Add(1, 300);
        piriceData.Add(2, 300);
        piriceData.Add(3, 300);
        piriceData.Add(4, 300);
        piriceData.Add(5, 500); // jacet
        piriceData.Add(6, 500); // pajama
        piriceData.Add(7, 500); // pajama
        piriceData.Add(8, 1000); // Dress
    }

    void Start()
    {
        
        Time.timeScale = 1;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();

        moneyText.text = "Money: $" + currentMoney;
        cartText.text = "Your cart: " + CartScript.cartId.Count.ToString() + "/6 [Q]";
    }

    public void Pause()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            menuPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            menuPanel.SetActive(false);
        }
    }

    public void Return()
    {
        Pause();
    }

    public void Replay(string currentSceneName)
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void MainMenu(string mainMenuScene)
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
