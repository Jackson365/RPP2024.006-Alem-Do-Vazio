using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text healthText;
    
    public static GameController instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    
    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
    }
}