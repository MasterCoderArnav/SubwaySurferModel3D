using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void restartGame()
    {
        FindObjectOfType<AudioManager>().stopSound("MainTheme");
        FindObjectOfType<AudioManager>().playSound("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quitGame()
    {
        FindObjectOfType<AudioManager>().stopSound("MainTheme");
        FindObjectOfType<AudioManager>().playSound("ButtonClick");
        SceneManager.LoadScene("MainMenu");
    }
}
