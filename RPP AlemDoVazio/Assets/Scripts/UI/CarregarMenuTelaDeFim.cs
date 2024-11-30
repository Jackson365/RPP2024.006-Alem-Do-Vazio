using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarMenuTelaDeFim : MonoBehaviour
{
    public string cenaCarregar;
    public GameObject MenuObj;
    
    void Start()
    {
        //MenuObj.SetActive(true);
        GameController.instance.MenuObj.SetActive(true);
        StartCoroutine(RestartMusicAfterSceneLoad());
        SceneManager.LoadScene(cenaCarregar);
    }
    
    private IEnumerator RestartMusicAfterSceneLoad()
    {
        yield return null;
        AudioObserver.OnPlayMusicEvent();
    }
}
