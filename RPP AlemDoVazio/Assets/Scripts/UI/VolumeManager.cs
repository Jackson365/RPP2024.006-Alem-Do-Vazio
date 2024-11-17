using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Button muteButton; // Botão para controlar o som
    public Sprite soundOnIcon; // Ícone para som ligado
    public Sprite soundOffIcon; // Ícone para som desligado

    private bool isMuted = false;
    private float lastVolume = 1.0f; // Volume antes de mutar

    private void Start()
    {
        // Configura o volume inicial
        lastVolume = VolumeObserver.CurrentVolume;
        isMuted = lastVolume == 0.0f;
        UpdateMuteButtonIcon();

        // Configura o evento de clique para o botão
        muteButton.onClick.AddListener(ToggleMute);
    }

    private void ToggleMute()
    {
        // Alterna o estado de isMuted
        isMuted = !isMuted;

        if (isMuted)
        {
            // Guarda o volume atual e define para 0 (mudo)
            lastVolume = VolumeObserver.CurrentVolume;
            VolumeObserver.CurrentVolume = 0.0f;
        }
        else
        {
            // Restaura o volume anterior
            VolumeObserver.CurrentVolume = lastVolume;
        }

        UpdateMuteButtonIcon();
    }

    private void UpdateMuteButtonIcon()
    {
        UpdateMuteButtonIcon(VolumeObserver.CurrentVolume); // Chama a versão com parâmetro
    }
    
    public void UpdateMuteButtonIcon(float newVolume)
    {
        // Atualiza o ícone do botão conforme o estado de som
        isMuted = VolumeObserver.CurrentVolume == 0.0f;
        
        Image buttonImage = muteButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = isMuted ? soundOffIcon : soundOnIcon;
        }
    }

    private void OnEnable()
    {
        VolumeObserver.VolumeChanged += UpdateAudioSources;
        VolumeObserver.VolumeChanged += UpdateMuteButtonIcon; // Inscreve-se para atualizar o ícone quando o volume muda
    }

    private void OnDisable()
    {
        VolumeObserver.VolumeChanged -= UpdateAudioSources;
        VolumeObserver.VolumeChanged -= UpdateMuteButtonIcon; // Inscreve-se para atualizar o ícone quando o volume muda
    }

    private void UpdateAudioSources(float newVolume)
    {
        // Aplica o volume global ao áudio
        AudioListener.volume = newVolume;
    }
}