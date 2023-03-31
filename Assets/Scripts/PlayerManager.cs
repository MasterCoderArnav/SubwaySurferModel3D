using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject gameStartedText;
    public static bool isGameStarted;
    public static int coinsCollected;
    public Text coinsText;
    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        coinsCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinsText.text = "Coins: " + coinsCollected;
        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(gameStartedText);
        }
    }
}
