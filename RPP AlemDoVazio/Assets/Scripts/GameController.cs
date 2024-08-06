using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Text healthText;

    // Start is called before the first frame update
    void Awake()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }  
    }
    
    public void UpdateLives(int value)
    {
        healthText.text = "x " + value.ToString();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            SceneManager.LoadScene("Level2");
        }
    }
}