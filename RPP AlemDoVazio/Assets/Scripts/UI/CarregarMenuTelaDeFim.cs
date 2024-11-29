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
        SceneManager.LoadScene(cenaCarregar);
        MenuObj.SetActive(true);
    }
}
