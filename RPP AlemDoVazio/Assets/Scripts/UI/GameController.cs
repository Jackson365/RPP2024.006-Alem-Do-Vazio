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

    public GameObject pauseObj;
    public GameObject GameOverObj;
    public GameObject configObj;

    private bool isPaused;

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
        
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void GameOver()
    {
        GameOverObj.SetActive(true);
    }
    

    public void RestartGame()
    {
        HealthObserver.ResetHealth();
        SceneManager.LoadScene(1);
        GameOverObj.SetActive(false);
    }

    public void ConfigGame()
    {
        configObj.SetActive(true);
    }
}