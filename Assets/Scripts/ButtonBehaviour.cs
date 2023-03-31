using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public void startGame(){
        FindObjectOfType<AudioManager>().stopSound("MainTheme");
        FindObjectOfType<AudioManager>().playSound("ButtonClick");
        SceneManager.LoadScene("Game");
    }
    public void quitGame()
    {
        FindObjectOfType<AudioManager>().stopSound("MainTheme");
        FindObjectOfType<AudioManager>().playSound("ButtonClick");
        Application.Quit();
    }
}
