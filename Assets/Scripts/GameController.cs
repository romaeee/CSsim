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


    void Start()
    {
        moneyText.text = "Money: $" + currentMoney;
        Time.timeScale = 1;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();

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
