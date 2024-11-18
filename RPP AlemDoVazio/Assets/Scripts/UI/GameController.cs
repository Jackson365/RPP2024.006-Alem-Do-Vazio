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
    public GameObject CanvasObj;
    public GameObject MenuObj;

    [Header("Play e Pause")]
    public Button playPauseButton; // Botão para controlar o Play/Pause
    public Sprite playIcon; // Ícone de Play
    public Sprite pauseIcon; // Ícone de Pause
    
    [Header("Bows")]
    public Image[] bowIcons; // UI icons para mostrar a flecha selecionada
    private int selectedBowIndex = 0;
    
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
    
    public bool IsGamePaused()
    {
        return isPaused;
    }

    private void Start()
    {
        totalAmulet = PlayerPrefs.GetInt("scoreAmulet");
        UpdatePlayPauseButtonIcon(); 
        playPauseButton.onClick.AddListener(TogglePlayPause); 
        
        selectedBowIndex = 0; 
        UpdateBowIcon(selectedBowIndex);
    }
    
    private void Update()
    {
        // Alterna o pause quando a tecla P é pressionada
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //TogglePlayPause();
        //}
    }
    
    public void UpdateBowIcon(int selectedIndex)
    {
        if (instance.IsGamePaused()) return;

        selectedBowIndex = selectedIndex;

        // Define as cores para o ícone ativo e os ícones inativos
        Color selectedColor = Color.white; // Cor para o ícone selecionado
        Color unselectedColor = new Color(1, 1, 1, 0f); // Cor para ícones não selecionados (totalmente transparente)

        // Atualiza a visibilidade e a cor dos ícones
        for (int i = 0; i < bowIcons.Length; i++)
        {
            if (bowIcons[i] != null)
            {
                // Se o índice atual for igual ao índice selecionado, muda a cor para o selecionado
                bowIcons[i].color = (i == selectedBowIndex) ? selectedColor : unselectedColor;

                // Torna o ícone visível ou invisível com base na seleção
                bowIcons[i].gameObject.SetActive(i == selectedBowIndex);
            }
        }
    }

    public void UpdateAmulet(int value)
    {
        scoreAmulet += value;
        amuleText.text = scoreAmulet.ToString();
        
        PlayerPrefs.SetInt("score", scoreAmulet + totalAmulet);
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
        Time.timeScale = 0;
        
        //Faz o som parar apos morrer
        AudioObserver.OnStopMusicEvent();
    }

    public void RestartGame()
    {
        isPaused = false; 
        UpdatePlayPauseButtonIcon();
        
        Time.timeScale = 1;
        
        selectedBowIndex = 0; 
        UpdateBowIcon(selectedBowIndex);
        
        HealthObserver.ResetHealth();
        GameOverObj.SetActive(false);
        
        SceneManager.LoadScene(1);
        StartCoroutine(RestartMusicAfterSceneLoad());
        
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetBowSelection();
        }
    }
    
    public void ExitGameMenu()
    {
        isPaused = false; 
        UpdatePlayPauseButtonIcon();
        
        Time.timeScale = 1;
        
        selectedBowIndex = 0; 
        UpdateBowIcon(selectedBowIndex);
        
        GameOverObj.SetActive(false);
        pauseObj.SetActive(false);

        SceneManager.LoadScene(0);
        StartCoroutine(RestartMusicAfterSceneLoad());
        
        //Volta a vida á quantidade inicial
        HealthObserver.ResetHealth();
        
        VolumeObserver.CurrentVolume = 1.0f;
        
        CanvasObj.SetActive(false);
        MenuObj.SetActive(true);
        
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetBowSelection();
        }
    }
    
    private IEnumerator RestartMusicAfterSceneLoad()
    {
        yield return null;
        AudioObserver.OnPlayMusicEvent();
    }
    
    public void ExitGame()
    {
        Debug.Log("Sair do jogo acionado!");
        Application.Quit();
    }
}
