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

    public static Dictionary<int, int> priceData = new Dictionary<int, int>();

    private void Awake()
    {
        priceData.Add(0, 300);
        priceData.Add(1, 300);
        priceData.Add(2, 300);
        priceData.Add(3, 300);
        priceData.Add(4, 300);
        priceData.Add(5, 500); // jacet
        priceData.Add(6, 500); // pajama
        priceData.Add(7, 500); // pajama
        priceData.Add(8, 1000); // Dress
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
        GameController.priceData.Clear();
        SceneManager.LoadScene(currentSceneName);
    }

    public void MainMenu(string mainMenuScene)
    {
        GameController.priceData.Clear();
        SceneManager.LoadScene(mainMenuScene);
    }
}
