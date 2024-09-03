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

    private int scoreAmulet;
    public Text amuleText;
    private int totalAmulet;

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

    private void Start()
    {
        totalAmulet = PlayerPrefs.GetInt("scoreAmulet");
    }

    public void UpdateAmulet(int value)
    {
        scoreAmulet += value;
        amuleText.text = scoreAmulet.ToString();
        
        PlayerPrefs.SetInt("score", scoreAmulet + totalAmulet);
    }
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            SceneManager.LoadScene("Level2");
        }
    }
}