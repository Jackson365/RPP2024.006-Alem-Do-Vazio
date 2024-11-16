using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Atributtes")]
    private int scoreAmulet;
    public Text amuleText;
    private int totalAmulet;

    [Header("GameObjects")]
    public GameObject pauseObj;
    public GameObject GameOverObj;

    [Header("Play e Pause")]
    public Button playPauseButton; // Botão para controlar o Play/Pause
    public Sprite playIcon; // Ícone de Play
    public Sprite pauseIcon; // Ícone de Pause

    private bool isPaused;

    void Awake()
    {
        if (instance == null)
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
        UpdatePlayPauseButtonIcon(); // Inicializa o botão com o ícone correto
        playPauseButton.onClick.AddListener(TogglePlayPause); // Configura o evento do botão
    }

    public void UpdateAmulet(int value)
    {
        scoreAmulet += value;
        amuleText.text = scoreAmulet.ToString();
        
        PlayerPrefs.SetInt("score", scoreAmulet + totalAmulet);
    }

    private void Update()
    {
        // Alterna o pause quando a tecla P é pressionada
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePlayPause();
        }
    }

    private void TogglePlayPause()
    {
        // Alterna o estado de pausa
        isPaused = !isPaused;
        pauseObj.SetActive(isPaused);

        // Define o tempo do jogo com base no estado
        Time.timeScale = isPaused ? 0 : 1;

        // Atualiza o ícone do botão
        UpdatePlayPauseButtonIcon();
    }

    private void UpdatePlayPauseButtonIcon()
    {
        // Atualiza o ícone do botão conforme o estado de pausa
        Image buttonImage = playPauseButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = isPaused ? playIcon : pauseIcon;
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
    
    public void ExitGame()
    {
        Debug.Log("Sair do jogo acionado!");
        Application.Quit();
    }
}
