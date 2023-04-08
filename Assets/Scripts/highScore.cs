using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScore : MonoBehaviour
{
    public Text highScoreText;
    void Start()
    {
        highScoreText.text = "High score: " + PlayerPrefs.GetInt("High Score");
    }
    void Update()
    {
        
    }
}
